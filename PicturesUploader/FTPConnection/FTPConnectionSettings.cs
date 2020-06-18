using System;
using System.Net;

namespace PicturesUploader.FTPConnection
{
    public delegate void FTPConnectionSettingsChangedHandler(FTPConnectionSettings settings);
    public class FTPConnectionSettings
    {
        public static event FTPConnectionSettingsChangedHandler FTPSettingsChanged;
        public NetworkCredential Credential { get; }
        private Uri _url;
        public Uri URL {
            get { return this._url; }
            private set {
                if (value == null)
                {
                    this._url = null;
                    return;
                }

                if (!value.IsAbsoluteUri)
                    throw new Exception($"Url {value.AbsoluteUri} is not absolute");

                string absoluteUri = value.AbsoluteUri.TrimStart('/');
                char lastChar = absoluteUri[absoluteUri.Length - 1];
                if (lastChar != '/')
                    this._url = new Uri(absoluteUri + "/");
                else
                    this._url = value;

            }
        }        
        public string ExternalWWWFolder { get; }
        public FTPConnectionSettings(string server, string user, string password, string externalFolder) 
            : this(new Uri(server), new NetworkCredential(user, password), externalFolder) { }        
        public FTPConnectionSettings(Uri url, NetworkCredential credential, string externalFolder)
        {
            this.URL = url;
            this.Credential = credential;
            this.ExternalWWWFolder = externalFolder;
        }

        internal static FTPConnectionSettings LoadSettings()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.FTP_Server))
                return null;

            try
            {
                return new FTPConnectionSettings
                (
                server: Cryptography.DecryptString(Properties.Settings.Default.FTP_Server),
                user: Cryptography.DecryptString(Properties.Settings.Default.FTP_Login),
                password: Cryptography.DecryptString(Properties.Settings.Default.FTP_Password),
                externalFolder: Cryptography.DecryptString(Properties.Settings.Default.WWW_ExternalFolder)
                );
            }
            catch 
            {
                return null;                
            }

            
        }        
        internal static void SaveSettings(FTPConnectionSettings settings)
        {
            if (settings == null)
            {
                Properties.Settings.Default.FTP_Server = Properties.Settings.Default.FTP_Login = Properties.Settings.Default.FTP_Password = Properties.Settings.Default.WWW_ExternalFolder = null;
            }
            else
            {
                Properties.Settings.Default.FTP_Server = Cryptography.EncryptString(settings.URL.AbsoluteUri);
                Properties.Settings.Default.FTP_Login = Cryptography.EncryptString(settings.Credential.UserName);
                Properties.Settings.Default.FTP_Password = Cryptography.EncryptString(settings.Credential.Password);
                Properties.Settings.Default.WWW_ExternalFolder = Cryptography.EncryptString(settings.ExternalWWWFolder);
            }            
            Properties.Settings.Default.Save();
            FTPSettingsChanged?.Invoke(settings);
        }       
    }
}
