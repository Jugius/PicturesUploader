using System;
using System.Windows.Forms;
using PicturesUploader.Office;
using System.Linq;

namespace PicturesUploader
{
    public partial class MainForm : Form
    {
        private ExcelWorkBookInfo _openedExcelFile = null;
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
            cmbSheets.Enabled = cmbNames.Enabled = cmbLinks.Enabled = txtBeginRow.Enabled = txtEndRow.Enabled = enabled;
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
            string path = ExcelStatic.OpenExcelFileDialog();
            if (!string.IsNullOrEmpty(path))
            {
                OpenedExcelFile = null;
                ReadExcelFileInfo(path);
            }
        }
    }
}
