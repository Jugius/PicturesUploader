using PicturesUploader.FTPConnection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ComponentModel;

namespace PicturesUploader.Uploaders
{
    class UploaderFTP : Uploader
    {
        public FTPConnectionSettings FTPSettings { get; set; }
        private string RootFolder { get; set; }

        public override void RunUpload(IEnumerable<PictureItem> items, BackgroundWorker bw)
        {
            this.BW = bw;
            this.RootFolder = FTPSettings.Server + FTPSettings.InternalFTPFolder;
            CreateDirectory(this.RootFolder);
            string rootF = RootFolder + @"/" + this.UploadFolder;
            CreateDirectory(rootF);
            RootFolder = rootF + @"/";

            BW.ReportProgress(0, "Сохранение фото...");

            ProgressTicker ticker = new ProgressTicker(items.Count(), 20);
            ticker.ProgressChanged += Ticker_ProgressChanged;
            foreach (var item in items)
            {
                try
                {
                    if (!ImageDownloadHelper.IsUrlImage((item.Address)))
                        throw new Exception("Скачиваемый контент не является графическим изображением.");

                    byte[] imageByteArray = ImageDownloadHelper.DownloadData(item.Address);
                    string ext = imageByteArray.GetExtention();
                    string pictureName = $"{item.Name}.{ext}";
                    item.Address = UploadPicture(imageByteArray, pictureName);
                    item.Status = true;
                    ticker.Tick();
                }
                catch (Exception ex)
                {
                    item.Status = false;
                    item.Address += $" Ошибка: {ex.Message}.";
                }

            }
            ticker = null;
        }
        protected override void CreateDirectory(string directoryPath)
        {
            try
            {
                FtpWebRequest requestDir = WebRequest.Create(directoryPath) as FtpWebRequest;
                requestDir.Method = WebRequestMethods.Ftp.MakeDirectory;
                requestDir.Credentials = new NetworkCredential(FTPSettings.Login, FTPSettings.Password);
                requestDir.UsePassive = true;
                requestDir.UseBinary = true;
                requestDir.KeepAlive = false;
                FtpWebResponse response = (FtpWebResponse)requestDir.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                ftpStream.Close();
                response.Close();
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    response.Close();
                }
                else
                {
                    response.Close();
                    throw ex;
                }
            }
        }
        protected override string UploadPicture(byte[] imageByteArray, string pictureName)
        {
            FtpWebRequest request;
            request = WebRequest.Create(new Uri(RootFolder + pictureName)) as FtpWebRequest;
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.UseBinary = true;
            request.UsePassive = true;
            request.KeepAlive = false;
            request.Credentials = new NetworkCredential(FTPSettings.Login, FTPSettings.Password);

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(imageByteArray, 0, imageByteArray.Length);
                requestStream.Flush();
                requestStream.Close();
            }
            string webFileName = FTPSettings.ExternalWWWFolder + @"/" + this.UploadFolder + @"/" + pictureName;
            return webFileName;
        }
    }
}
