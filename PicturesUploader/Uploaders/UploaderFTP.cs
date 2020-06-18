using PicturesUploader.FTPConnection;
using System;
using NetworkClient;
using System.Drawing;
using System.Net;

namespace PicturesUploader.Uploaders
{
    class UploaderFTP : IUploader
    {        
        private FTPConnectionSettings FTPSettings;
        private FTPClientManager ftpManager;
        private FTPClient ftpClient;
        internal UploaderFTP(FTPConnectionSettings ftpsettings, string uploadFolder) : base(uploadFolder)
        {
            this.FTPSettings = ftpsettings;
        }
        internal override void Initialize()
        {
            ftpManager = new FTPClientManager(FTPSettings.URL, FTPSettings.Credential);
            ftpManager.CreateDirectory(FTPSettings.URL);
            ftpManager.CreateDirectory(this.UploadFolder);
            ftpClient = new FTPClient(ftpManager);
        }
        internal override Uri SaveImage(Image image, string imageName)
        {
            if (ftpClient == null)
                throw new Exception("ftpClient is not initialized");

            ImageConverter _imageConverter = new ImageConverter();
            byte[] imageByteArray = (byte[])_imageConverter.ConvertTo(image, typeof(byte[]));

            ftpClient.UploadData(imageByteArray, imageName);

            return new Uri (FTPSettings.ExternalWWWFolder + @"/" + this.UploadFolder + @"/" + imageName);
        }
    }
}
