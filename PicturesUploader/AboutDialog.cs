using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace PicturesUploader
{
    public partial class AboutDialog : Form
    {
        public const string ApplicationSite = @"http://oohelp.net/picturesuploader/";
        public const string SupportEmail = @"support@oohelp.net";

        public AboutDialog()
        {
            InitializeComponent();
        }
        public void ShowDialog(Updater.IUpdatableApplication app)
        {
            this.Text = $"О программе {app.ApplicationName}";
            lblProgramName.Text = app.ApplicationName;
            this.Icon = app.ApplicationIcon;
            pictureAppImage.Image = app.ApplicationImage;
            var version = app.Version;
            lblVersion.Text = "Версия: " + version.Major + "." + version.Minor + (version.Build != 0 ? $" (build {version.Build})" : null);
            lblCopyright.Text = GetAssemblyCopyright(app.ApplicationAssembly);
            linkWWW.Text = $"Страница {app.ApplicationName}";
            linkEmail.Text = "Написать нам";

            base.ShowDialog(app.Context);
        }
        private string GetAssemblyCopyright(Assembly assembly)
        {
            if (assembly == null) return "";
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            if (attributes.Length == 0)
            {
                return "";
            }
            return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;

        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string mailto = $"mailto:{AboutDialog.SupportEmail}?Subject=Сообщение {lblProgramName.Text}";
            mailto = System.Uri.EscapeUriString(mailto);
            System.Diagnostics.Process.Start(mailto);
        }
        private void _secondaryPanel_Paint(object sender, PaintEventArgs e)
        {
            VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("TASKDIALOG", 8, 0));
            renderer.DrawBackground(e.Graphics, _secondaryPanel.ClientRectangle, e.ClipRectangle);
        }

        private void linkWWW_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(AboutDialog.ApplicationSite);
        }
    }
}
