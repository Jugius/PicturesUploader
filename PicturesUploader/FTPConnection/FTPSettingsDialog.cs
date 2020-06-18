using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace PicturesUploader.FTPConnection
{
    public partial class FTPSettingsDialog : Form
    {
        public FTPConnectionSettings FTPSettings { get; private set; }
        public FTPSettingsDialog(FTPConnectionSettings ftpSettings)
        {            
            InitializeComponent();

            if (ftpSettings != null)
            {
                this.FTPSettings = ftpSettings;
                txtServer.Text = $"{ftpSettings.URL.Scheme}://{ftpSettings.URL.Host}:{ftpSettings.URL.Port}" ;
                txtLogin.Text = ftpSettings.Credential.UserName;
                txtPassword.Text = ftpSettings.Credential.Password;
                
                txtInternalFTPFolder.Text = ftpSettings.URL.AbsolutePath != "/" ? ftpSettings.URL.AbsolutePath.TrimStart('/') : null;
                txtExternalWWWFolder.Text = FTPSettings.ExternalWWWFolder;
            }
            else
            {
                txtServer.Text = @"ftp://localhost";
            }
        }
        private bool IsValidated()
        {
            if (string.IsNullOrEmpty(txtServer.Text) && string.IsNullOrEmpty(txtLogin.Text) && string.IsNullOrEmpty(txtPassword.Text) && string.IsNullOrEmpty(txtInternalFTPFolder.Text) && string.IsNullOrEmpty(txtExternalWWWFolder.Text))
            {
                this.FTPSettings = null;
                return true;
            }
            try
            {
                string server = txtServer.Text.Trim();

                if (!server.Contains(@"ftp://"))
                    server = @"ftp://" + server;

                Uri url = new Uri(server);

                string addFolder = txtInternalFTPFolder.Text.Trim();
                if (addFolder != "/")
                    url = new Uri(Path.Combine(url.AbsoluteUri, addFolder.TrimStart('/')));


                NetworkCredential credential;

                if (!url.Scheme.Equals("ftp", StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("The schema of url must be ftp. ");
                }

                if (url.IsFile)
                {
                    url = new Uri(url, "..");
                }

                if (String.IsNullOrEmpty(txtLogin.Text)
                        || String.IsNullOrEmpty(txtPassword.Text))
                {
                    throw new Exception("Please type the user name and password!");
                }
                else
                {
                    credential = new NetworkCredential(
                        txtLogin.Text.Trim(),
                        txtPassword.Text);
                }
                this.FTPSettings = new FTPConnection.FTPConnectionSettings(url, credential, txtExternalWWWFolder.Text);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (IsValidated())
                this.DialogResult = DialogResult.OK;
        }

        private void btnCheckConnection_Click(object sender, EventArgs e)
        {
            if (!IsValidated())
                return;

            try
            {
                if (NetworkClient.FTPClientManager.VerifyFTPUrlExist(this.FTPSettings.URL, this.FTPSettings.Credential))
                {
                    Uri newUri = new Uri(this.FTPSettings.URL, Guid.NewGuid().ToString() + "/");
                    NetworkClient.FTPClientManager manager = new NetworkClient.FTPClientManager(this.FTPSettings.URL, this.FTPSettings.Credential);
                    manager.CreateDirectory(newUri);
                    manager.DeleteDirectory(newUri);
                    MessageBox.Show("Соединение успешно.", "Соединение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string message = "Ошибка. Адрес FTP не существует. Создайте папку на сервере.\n\n" + this.FTPSettings.URL;
                    MessageBox.Show(message, "Соединение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                string message = "Ошибка соединения. Ответ сервера:" + Environment.NewLine + (ex.InnerException != null ? ex.InnerException.Message : ex.Message);
                MessageBox.Show(message, "Соединение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
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
    }
}
