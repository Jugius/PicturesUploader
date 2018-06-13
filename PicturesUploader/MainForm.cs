using System;
using System.Windows.Forms;
using PicturesUploader.Office;
using System.Linq;
using System.ComponentModel;
using PicturesUploader.FTPConnection;
using PicturesUploader.Uploaders;

namespace PicturesUploader
{
    public partial class MainForm : Form
    {
        private BackgroundWorker BWorker;
        private ExcelWorkBookInfo _openedExcelFile = null;
        private Processor Processor = null;
        private ExcelWorkBookInfo OpenedExcelFile
        {
            get { return _openedExcelFile; }
            set {
                _openedExcelFile = value;
                if (value == null)
                {
                    lblFileName.Text = "Открыть файл";
                    lblFileName.LinkColor = System.Drawing.Color.Red;
                    SetControlsEnabled(false);
                }
                else
                {
                    lblFileName.Text = System.IO.Path.GetFileName(value.Path);
                    lblFileName.LinkColor = System.Drawing.SystemColors.Highlight;
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
        }
        private void ReadExcelFileInfo(string path)
        {
            ExcelWorkBookInfo fileInfo = null;
            try
            {
                using (UsingExcel xls = new UsingExcel())
                {
                    fileInfo = xls.ReadExcelFileInfo(path);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка чтения Excel"); }
            this.OpenedExcelFile = fileInfo;
        }
        private void SetControlsEnabled(bool enabled)
        {
            cmbSheets.Enabled = cmbNames.Enabled = cmbLinks.Enabled = txtBeginRow.Enabled
                = txtEndRow.Enabled = rbSaveFTP.Enabled = rbSaveLocal.Enabled
                = txtPictureFolderName.Enabled = enabled;
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

        private void lblFileName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
            else {
                OpenedExcelFile = null;
            }
            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try {
                if (this.OpenedExcelFile == null)
                    throw new Exception("Необходимо открыть файл Excel.");

                ExcelWorkSpaceInfo excelInfo = new Office.ExcelWorkSpaceInfo(this.OpenedExcelFile, (int)cmbSheets.SelectedValue);
                excelInfo.ColumnPictureNames = cmbNames.Text;
                excelInfo.ColumnPictureHyperlinks = cmbLinks.Text;
                excelInfo.RowBeginUpload = Convert.ToInt32(txtBeginRow.Text);
                excelInfo.RowEndUpload = Convert.ToInt32(txtEndRow.Text);

                var param = new ProcessorParameters
                {
                    ExcelInfo = excelInfo,
                    Direction = rbSaveLocal.Checked ? UploadPicturesDirection.LOCAL : UploadPicturesDirection.FTP,
                    UploadFolderName = string.IsNullOrEmpty(txtPictureFolderName.Text) ? Guid.NewGuid().ToString() : txtPictureFolderName.Text
                };
                
                SetControlsEnabled(false);

                this.BWorker = new BackgroundWorker();
                this.Processor = new Processor();
                BWorker.WorkerReportsProgress = true;
                BWorker.WorkerSupportsCancellation = false;
                BWorker.ProgressChanged += BWorker_ProgressChanged;
                BWorker.RunWorkerCompleted += BWorker_RunWorkerCompleted;
                BWorker.DoWork += this.Processor.RunProcess;
                
                BWorker.RunWorkerAsync(param);

            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void BWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Ошибка выполнения", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                lblStatus.Text = "Завершено успешно";
            }
            SetControlsEnabled(true);
            
        }

        private void BWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage >= 0)
                pbProgress.Value = e.ProgressPercentage;

            if (e.UserState != null)
                lblStatus.Text = e.UserState.ToString();
        }

        private void btnSettings_Click(object sender, EventArgs e)
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
    }
}
