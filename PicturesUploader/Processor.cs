using PicturesUploader.Office;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PicturesUploader
{    
    class Processor
    {
        private BackgroundWorker BW;
        private ProcessorParameters Parameters;
        public void RunProcess(object sender, DoWorkEventArgs e)
        {
            this.BW = sender as BackgroundWorker;
            this.Parameters = e.Argument as ProcessorParameters;

            //Loading Items from excel file
            List<PictureItem> items;
            BW.ReportProgress(0, "Считываем таблицу...");
            using (UsingExcel xls = new UsingExcel())
            {
                items = xls.GetPhotoItems(this.Parameters.ExcelInfo);
            }
            if (items == null || items.Count == 0)
            {
                throw new Exception("Нет записей в списке");
            }

            string uploadDir = string.IsNullOrEmpty(Parameters.UploadDirectory) ? Guid.NewGuid().ToString() : Parameters.UploadDirectory;

            if (Parameters.UploadDirection == Uploaders.UploadDirection.LOCAL)
                uploadDir = System.IO.Path.GetDirectoryName(Parameters.ExcelInfo.FilePath) + "\\" + uploadDir;

            Uploaders.UploaderBuilder builder = new Uploaders.UploaderBuilder(uploadDir);
            Uploaders.IUploader uploader = builder.Build(Parameters.UploadDirection);

            uploader.Initialize();

            BW.ReportProgress(0, "Сохранение фото...");
            ProgressTicker ticker = new ProgressTicker(items.Count, 1);
            ticker.ProgressChanged += Ticker_ProgressChanged;

            List<PictureItem> uploadedItems = new List<PicturesUploader.PictureItem>();

            foreach (var item in items)
            {
                if (BW.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                if (item.Error == null)
                {
                    try
                    {
                        ImageResizer.ImageInfo image = ImageResizer.ImageInfo.Build(item.Address);
                        image.ResizeImage(this.Parameters.ImageResizeSettings);
                        string pictureName = $"{item.Name}.{image.SourceExtention}";
                        item.Address = uploader.SaveImage(image.DestinationBitmap, pictureName);                        
                    }
                    catch (ImageResizer.ImageCorruptedException)
                    {
                        item.Error = new Exception("Не прямая ссылка на изображение, либо файл поврежден");
                    }
                    catch (Exception ex)
                    {
                        item.Error = ex;
                    }
                }
                uploadedItems.Add(item);
                ticker.Tick();
            }


            using (UsingExcel xls = new UsingExcel())
            {
                BW.ReportProgress(0, "Записываем результат в Excel файл");
                xls.UpdatePhotoItems(uploadedItems, this.Parameters.ExcelInfo);
            }
            e.Result = items.Count;
        }

        private void Ticker_ProgressChanged(ProgressData data)
        {
            this.BW.ReportProgress(data.Progress, $"Выполнено: {data.TicksDone} из {data.TicksTotal}");
        }
    }
}
