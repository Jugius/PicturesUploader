using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace PicturesUploader
{
    public partial class AboutDialog : Form
    {
        private const string SoftwareSiteBase = @"https://oohelp.net/software";
        private const string SupportEmail = "jugius@gmail.com";

        public AboutDialog()
        {
            InitializeComponent();
        }
        public AboutDialog(MainForm app) : this()
        {
            this.Text = $"О программе {app.ApplicationName}";
            lblProgramName.Text = app.ApplicationName;
            this.Icon = app.Icon;
            lblVersion.Text = GetFormattedVersionString(app.Version);
            lblCopyright.Text = GetAssemblyCopyright(Assembly.GetExecutingAssembly());
            linkWWW.Text = $"Страница {app.ApplicationName}";
            linkWWW.LinkClicked += (sender, e) => { Process.Start($"{SoftwareSiteBase}/{app.ApplicationName.ToLower()}"); };
        }      

        private void _secondaryPanel_Paint(object sender, PaintEventArgs e)
        {
            VisualStyleRenderer renderer = new VisualStyleRenderer(VisualStyleElement.CreateElement("TASKDIALOG", 8, 0));
            renderer.DrawBackground(e.Graphics, _secondaryPanel.ClientRectangle, e.ClipRectangle);
        }
        private static string GetAssemblyCopyright(Assembly assembly)
        {
            if (assembly == null) return "";
            object[] attributes = assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            if (attributes.Length == 0)
            {
                return "";
            }
            return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;

        }
        private static string GetFormattedVersionString(Version version)
        {
            string v = $"Версия: {version.Major}.{version.Minor}";
            if (version.Build != 0)
            {
                v += $" (build {version.Build}";
                if (version.Revision > 0)
                {
                    v += $" rev. {version.Revision}";
                }
                v += ")";
            }
            return v;
        }
        public static string GetSupportEmailProcessString(string appName) =>
            $"mailto:{SupportEmail}?Subject=Support request for {appName}";
    }
}
