using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace PicturesUploader
{
    public partial class ImageSettingsDialog : Form
    {
        public ImageSettingsDialog()
        {
            InitializeComponent();
            InitializeSizesComboBox();
        }
        private void InitializeSizesComboBox()
        {
            var sizes = ImageResizer.ImageSize.GetDefaults().OrderBy(a => a.ID).ToList();
            cmbSizes.DisplayMember = "Description";
            cmbSizes.ValueMember = "ID";
            cmbSizes.DataSource = sizes;
        }

        private void _primaryPanel_Paint(object sender, PaintEventArgs e)
        {
            DrawThemeBackground(e.Graphics, VisualStyleElement.CreateElement("TASKDIALOG", 1, 0), _primaryPanel.ClientRectangle, e.ClipRectangle);
        }

        private void _secondaryPanel_Paint(object sender, PaintEventArgs e)
        {
            DrawThemeBackground(e.Graphics, VisualStyleElement.CreateElement("TASKDIALOG", 8, 0), _secondaryPanel.ClientRectangle, e.ClipRectangle);
        }

        private static void DrawThemeBackground(IDeviceContext dc, VisualStyleElement element, Rectangle bounds, Rectangle clipRectangle)
        {
            VisualStyleRenderer renderer = new VisualStyleRenderer(element);
            renderer.DrawBackground(dc, bounds, clipRectangle);
        }

        private void ImageSettingsDialog_Load(object sender, System.EventArgs e)
        {
            if (PicturesUploader.Properties.Settings.Default.NewImageSize == 0)
            {
                rb_SaveImageSize.Checked = true;
                cmbSizes.Enabled = false;
            }
            else
            {
                rb_ChangeImageSize.Checked = true;
                cmbSizes.SelectedValue = PicturesUploader.Properties.Settings.Default.NewImageSize;
                cmbSizes.Enabled = true;
            }
            rb_ChangeImageSize.CheckedChanged += Rb_ImageSize_CheckedChanged;            
            rb_SaveImageSize.CheckedChanged += Rb_ImageSize_CheckedChanged;
        }

        private void Rb_ImageSize_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.Checked)
            {
                cmbSizes.Enabled = rb.Name == "rb_ChangeImageSize";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (rb_SaveImageSize.Checked)
            {
                PicturesUploader.Properties.Settings.Default.NewImageSize = 0;
            }
            else
            {
                PicturesUploader.Properties.Settings.Default.NewImageSize = (int)cmbSizes.SelectedValue;
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}
