using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;


namespace PicturesUploader.Uploaders
{
    class Uploader
    {
        protected BackgroundWorker BW;
        public string UploadFolder { get; set; }
        public virtual void RunUpload(IEnumerable<PictureItem> items, BackgroundWorker bw)
        {
            this.BW = bw;
            CreateDirectory(UploadFolder);
            BW.ReportProgress(0, "Сохранение фото...");
            ProgressTicker ticker = new ProgressTicker(items.Count(), 20);
            ticker.ProgressChanged += Ticker_ProgressChanged;
            foreach (var item in items)
            {
                try {
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

        protected void Ticker_ProgressChanged(ProgressData data)
        {
            this.BW.ReportProgress(data.Progress, $"Выполнено: {data.TicksDone} из {data.TicksTotal}");
        }

        protected virtual void CreateDirectory(string directoryPath)
        {
            if (!System.IO.Directory.Exists(directoryPath))
            {
                System.IO.Directory.CreateDirectory(directoryPath);
            }
        }

        protected virtual string UploadPicture(byte[] imageByteArray, string pictureName)
        {
            string destinationfile = UploadFolder + @"\" + pictureName;
            System.IO.File.WriteAllBytes(destinationfile, imageByteArray);
            return destinationfile;
        }
                
    }
}
