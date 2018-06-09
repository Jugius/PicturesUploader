using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace PicturesUploader.FTPConnection
{
    public partial class FTPSettingsDialog : Form
    {
        public FTPConnectionSettings FTPSettings { get; private set; }
        public FTPSettingsDialog(FTPConnectionSettings ftpSettings)
        {
            this.FTPSettings = (FTPConnectionSettings)ftpSettings.Clone();
            InitializeComponent();
            txtServer.Text = FTPSettings.Server;
            txtLogin.Text = FTPSettings.Login;
            txtPassword.Text = FTPSettings.Password;
            txtInternalFTPFolder.Text = FTPSettings.InternalFTPFolder;
            txtExternalWWWFolder.Text = FTPSettings.ExternalWWWFolder;
        }
        private bool IsValidated()
        {
            try
            {
                string server = txtServer.Text.Trim();

                if (string.IsNullOrEmpty(server))
                    throw new Exception("Адрес сервера должен быть обязательно указан.");

                FTPSettings.Server = server;
                FTPSettings.Login = txtLogin.Text;
                FTPSettings.Password = txtPassword.Text;
                FTPSettings.InternalFTPFolder = txtInternalFTPFolder.Text;
                FTPSettings.ExternalWWWFolder = txtExternalWWWFolder.Text;
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

            string status;
            if (TryToConnect(out status))
                MessageBox.Show("Соединение успешно.", "Соединение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                string message = "Ошибка соединения. Ответ сервера:" + Environment.NewLine + status;
                MessageBox.Show(message, "Соединение", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool TryToConnect(out string status)
        {
            status = null;
            string rootFolder = FTPSettings.Server + FTPSettings.InternalFTPFolder;

            FtpWebRequest requestDir = (FtpWebRequest)FtpWebRequest.Create(rootFolder);
            requestDir.Method = WebRequestMethods.Ftp.MakeDirectory;
            requestDir.Credentials = new NetworkCredential(FTPSettings.Login, FTPSettings.Password);
            requestDir.UsePassive = true;
            requestDir.UseBinary = true;
            requestDir.KeepAlive = false;
            try
            {
                WebResponse response = requestDir.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
                return true;
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    response.Close();
                    return true;
                }
                else
                {
                    response.Close();
                    status = ex.Message;
                    return false;
                }
            }
        }
    }
}
