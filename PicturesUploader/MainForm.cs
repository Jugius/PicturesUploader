using System;
using System.Windows.Forms;
using PicturesUploader.Office;
using System.Linq;
using System.ComponentModel;
using PicturesUploader.FTPConnection;
using PicturesUploader.Uploaders;
using System.Drawing;
using SoftwareManagement.Updater;
using System.Threading.Tasks;

namespace PicturesUploader
{
    public partial class MainForm : Form//, Updater.IUpdatableApplication
    {
        private BackgroundWorker BWorker;
        private ExcelFileInfo _openedExcelFile = null;
        private ProcessorParameters PrevFinishedParameters = null;

        private readonly ApplicationDeployment Updater;
        private ExcelFileInfo OpenedExcelFile
        {
            get { return _openedExcelFile; }
            set {
                _openedExcelFile = value;
                lblStatus.Text = null;
                groupBox4.Enabled = false;
                pbProgress.Value = 0;
                if (value == null)
                {
                    btnStart.Enabled = false;
                    lblFileName.Text = null;
                    SetControlsEnabled(false);
                    cmbSheets.SelectedValueChanged -= new System.EventHandler(this.cmbSheets_SelectedValueChanged);
                    cmbSheets.Text = cmbNames.Text = cmbLinks.Text= txtBeginRow.Text = txtEndRow.Text = null;
                    lblNumberOfColumns.Text = "Столбцов:";
                    lblNumberOfRows.Text = "Строк:";
                }
                else
                {
                    btnStart.Enabled = true;
                    lblFileName.Text = System.IO.Path.GetFileName(value.Path);                    
                    cmbSheets.SelectedValueChanged -= new System.EventHandler(this.cmbSheets_SelectedValueChanged);
                    cmbSheets.DataSource = value.Sheets;
                    cmbSheets.SelectedValueChanged += new System.EventHandler(this.cmbSheets_SelectedValueChanged);
                    SetControlsEnabled(true);
                    cmbSheets_SelectedValueChanged(null, null);
                }                
            }
        }    
        public MainForm()
        {            
            InitializeComponent();
            this.Icon = Properties.Resources.Hard_Disk_Server_icon1;
            OpenedExcelFile = null;
            btnQuickLoad.Enabled = FTPConnectionSettings.LoadSettings() != null;
            FTPConnectionSettings.FTPSettingsChanged += FTPConnectionSettings_FTPSettingsChanged;

            this.Updater = new ApplicationDeployment(this);
        }

        private void FTPConnectionSettings_FTPSettingsChanged(FTPConnectionSettings settings)
        {
            btnQuickLoad.Enabled = settings != null;
        }

        private void ReadExcelFileInfo(string path)
        {
            ExcelFileInfo fileInfo = null;
            try
            {
                using (UsingExcel xls = new UsingExcel())
                {
                    fileInfo = xls.ReadExcelFileInfo(path);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex, "Ошибка чтения Excel");
            }
            this.OpenedExcelFile = fileInfo;
        }
        private void SetControlsEnabled(bool enabled)
        {
            cmbSheets.Enabled = cmbLinks.Enabled = txtBeginRow.Enabled
                = txtEndRow.Enabled = rbSaveFTP.Enabled = rbSaveLocal.Enabled
                = txtPictureFolderName.Enabled = btnRefresh.Enabled
                = checkbUseNames.Enabled = enabled;
            cmbNames.Enabled = checkbUseNames.Checked && checkbUseNames.Enabled; 
        }
        private void cmbSheets_SelectedValueChanged(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(cmbSheets.SelectedValue);
            Office.ExcelSheet sh = OpenedExcelFile.Sheets.FirstOrDefault(a => a.Index == i);
            lblNumberOfColumns.Text = $"Столбцов: {sh.LastCell.Column}";
            lblNumberOfRows.Text = $"Строк: {sh.LastCell.Row}";
            cmbLinks.DataSource = ExcelStatic.GetColumnNames(sh.LastCell.Column);
            cmbNames.DataSource = ExcelStatic.GetColumnNames(sh.LastCell.Column);
            txtBeginRow.Text = "2";
            txtEndRow.Text = sh.LastCell.Row.ToString();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (this.BWorker != null && BWorker.IsBusy)
            {
                MessageBox.Show("Дождитесь завершения операции.", "Программа в процессе..", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string path = ExcelStatic.OpenExcelFileDialog();
            if (!string.IsNullOrEmpty(path))
            {
                ReadExcelFileInfo(path);
            }
            else
            {
                OpenedExcelFile = null;
            }
        }
        private ProcessorParameters GetProcessorParameters()
        {
            if (this.OpenedExcelFile == null)
                throw new Exception("Файл не загружен");

            Office.ExcelSheet selectedSheet = OpenedExcelFile.Sheets.FirstOrDefault(a => a.Index == Convert.ToInt32(cmbSheets.SelectedValue));
            string columnWithNames = checkbUseNames.Checked ? cmbNames.Text : null;
            string columnWithLinks = cmbLinks.Text;
            int rowBegin = Convert.ToInt32(txtBeginRow.Text);
            int rowEnd = Convert.ToInt32(txtEndRow.Text);

            if (rowBegin < 1 || rowBegin > selectedSheet.LastCell.Row || rowEnd < rowBegin || rowEnd > selectedSheet.LastCell.Row)
                throw new Exception("Указан неверный диапазон строк");

            UploadingExcelParameters excel = new Office.UploadingExcelParameters(OpenedExcelFile.Path, selectedSheet.Index, columnWithNames, columnWithLinks, rowBegin, rowEnd);
            excel.IncludeLinkToCell = this.chkLinkToCell.Checked;
            ProcessorParameters param = new ProcessorParameters(excel);
            param.UploadDirectory = txtPictureFolderName.Text.Trim();
            param.UploadDirection = rbSaveLocal.Checked ? UploadDirection.LOCAL : UploadDirection.FTP;

            return param;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var b = sender as Button;
            if (b.Text == "Отмена")
            {
                if (this.BWorker != null && BWorker.IsBusy)
                {
                    b.Enabled = false;
                    pbProgress.Style = ProgressBarStyle.Marquee;
                    this.BWorker.CancelAsync();
                }
            }
            else
            {
                try
                {
                    ProcessorParameters param = GetProcessorParameters();
                    if (param.Equals(this.PrevFinishedParameters))
                    {
                        const string mess = "Вы хотите начать загрузку с теми же параметрами, как выполнили до этого. Вероятно, результат будет такой же.\n\nВы хотите продолжить?";
                        if (MessageBox.Show(mess, "Повторить?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                    }
                    b.Text = "Отмена";
                    BeginUploading(param);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void BeginUploading(ProcessorParameters param)
        {
            SetControlsEnabled(false);
            this.BWorker = new BackgroundWorker();
            var processor = new Processor();
            BWorker.WorkerReportsProgress = true;
            BWorker.WorkerSupportsCancellation = true;
            BWorker.ProgressChanged += BWorker_ProgressChanged;
            BWorker.RunWorkerCompleted += BWorker_RunWorkerCompleted;
            BWorker.DoWork += processor.RunProcess;

            groupBox4.Enabled = true;
            this.PrevFinishedParameters = null;

            BWorker.RunWorkerAsync(param);
        }

        private void BWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                ShowErrorMessage(e.Error, "Ошибка выполнения");                
            }
            else if (e.Cancelled)
            {
                lblStatus.Text = $"Отменено.";
            }
            else
            {
                lblStatus.Text = "Обработка файла завершена";
                this.PrevFinishedParameters = e.Result as ProcessorParameters;
            }
            System.Diagnostics.Process.Start(this.OpenedExcelFile.Path);
            SetControlsEnabled(true);
            btnStart.Text = "Начать";
            btnStart.Enabled = true;
            pbProgress.Style = ProgressBarStyle.Continuous;
        }

        private void BWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage >= 0)
                pbProgress.Value = e.ProgressPercentage;

            if (e.UserState != null)
                lblStatus.Text = e.UserState.ToString();
        }

        private void btnSettingsFTP_Click(object sender, EventArgs e)
        {
            FTPConnectionSettings ftp = FTPConnectionSettings.LoadSettings();
            using (FTPSettingsDialog dlg = new FTPSettingsDialog(ftp))
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    FTPConnectionSettings.SaveSettings(dlg.FTPSettings);
                }
            }
        }
        private void MainForm_Shown(object sender, EventArgs e)
        {
            Task.Run(() => Updater.UpdateApplication(UpdateMethod.Automatic));
        }

        private void btnQuickLoad_Click(object sender, EventArgs e)
        {
            using (QuickLoadDialog dlg = new QuickLoadDialog())
            {
                dlg.ShowDialog(this);
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (OpenedExcelFile == null)
                return;
            ReadExcelFileInfo(OpenedExcelFile.Path);
        }

        private void btnSettings_MouseDown(object sender, MouseEventArgs e)
        {
            Point screenPoint = btnSettings.PointToScreen(new Point(btnSettings.Left, btnSettings.Bottom));
            if (screenPoint.Y + mnuSettings.Size.Height > Screen.PrimaryScreen.WorkingArea.Height)
            {
                mnuSettings.Show(btnSettings, new Point(0, -mnuSettings.Size.Height));
            }
            else
            {
                mnuSettings.Show(btnSettings, new Point(0, btnSettings.Height));
            }
        }
        private void btnAbout_MouseDown(object sender, MouseEventArgs e)
        {
            Point screenPoint = btnSettings.PointToScreen(new Point(btnSettings.Left, btnSettings.Bottom));
            if (screenPoint.Y + mnuSettings.Size.Height > Screen.PrimaryScreen.WorkingArea.Height)
            {
                mnuAbout.Show(btnSettings, new Point(0, -mnuSettings.Size.Height));
            }
            else
            {
                mnuAbout.Show(btnSettings, new Point(0, btnSettings.Height));
            }
        }

        private void mnuSettingsImage_Click(object sender, EventArgs e) => new ImageSettingsDialog().ShowDialog();

        private void checkbUseNames_CheckedChanged(object sender, EventArgs e)
        {
            cmbNames.Enabled = checkbUseNames.Checked;
        }
        private void ShowErrorMessage(Exception ex, string caption)
        {
            string error = ex.Message;
            string details = null;
            if (ex.InnerException != null)
            {
                error += "\nОшибка: " + ex.InnerException.Message;
                if (!string.IsNullOrEmpty(ex.InnerException.StackTrace))
                    details = "StackTrace: " + ex.InnerException.StackTrace;
            }

            if (string.IsNullOrEmpty(details))
            {
                MessageBox.Show(error, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    var dialogTypeName = "System.Windows.Forms.PropertyGridInternal.GridErrorDlg";
                    var dialogType = typeof(Form).Assembly.GetType(dialogTypeName);

                    // Create dialog instance.
                    var dialog = (Form)Activator.CreateInstance(dialogType, new PropertyGrid());

                    // Populate relevant properties on the dialog instance.
                    dialog.Text = caption;
                    dialogType.GetProperty("Details").SetValue(dialog, details, null);
                    dialogType.GetProperty("Message").SetValue(dialog, error, null);

                    // Display dialog.
                    var result = dialog.ShowDialog();
                }
                catch
                {
                    MessageBox.Show(error + "\n\n" + details, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }            
        }

        private void mnuShowAbout_Click(object sender, EventArgs e)
        {
            new AboutDialog(this).ShowDialog();
        }

        private void mnuHelp_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://oohelp.net/picturesuploader/#pictureuploader_howto");
        }

        private void mnuSendLetter_Click(object sender, EventArgs e)
        {
            string mailto = $"mailto:{"jugius@gmail.com"}?Subject={"Message from app: PicturesUploader"}";
            mailto = System.Uri.EscapeUriString(mailto);
            System.Diagnostics.Process.Start(mailto);
        }

        private void mnuCheckUpdates_Click(object sender, EventArgs e) => Task.Run(() => this.Updater.UpdateApplication(UpdateMethod.Manual));
    }

}
