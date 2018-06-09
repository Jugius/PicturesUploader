using System;

namespace PicturesUploader.FTPConnection
{
    public class FTPConnectionSettings : ICloneable
    {
        public string Server { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string InternalFTPFolder { get; set; }
        public string ExternalWWWFolder { get; set; }

        internal static FTPConnectionSettings LoadSettings()
        {
            FTPConnectionSettings settings = new FTPConnectionSettings
            {
                Server = Properties.Settings.Default.FTP_Server,
                Login = Properties.Settings.Default.FTP_Login,
                Password = Properties.Settings.Default.FTP_Password,
                InternalFTPFolder = Properties.Settings.Default.FTP_InternalFolder,
                ExternalWWWFolder = Properties.Settings.Default.WWW_ExternalFolder
            };
            return settings;
        }
        internal static void SaveSettings(FTPConnectionSettings settings)
        {
            Properties.Settings.Default.FTP_Server = settings.Server;
            Properties.Settings.Default.FTP_Login = settings.Login;
            Properties.Settings.Default.FTP_Password = settings.Password;
            Properties.Settings.Default.FTP_InternalFolder = settings.InternalFTPFolder;
            Properties.Settings.Default.WWW_ExternalFolder = settings.ExternalWWWFolder;
            Properties.Settings.Default.Save();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
