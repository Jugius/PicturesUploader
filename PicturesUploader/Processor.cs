using PicturesUploader.Office;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicturesUploader
{
    internal enum UploadPicturesDirection { FTP = 1, LOCAL = 2 }
    class Processor
    {
        private BackgroundWorker BW;
        public ExcelWorkSpaceInfo ExcelInfo { get; private set; }
        public UploadPicturesDirection Direction { get; private set; }
        public string UploadFolderName { get; set; }
        public Processor(ExcelWorkSpaceInfo excelInfo, UploadPicturesDirection uploadDirection)
        {
            this.ExcelInfo = excelInfo;
            this.Direction = uploadDirection;
        }
        public void RunProcess(object sender, DoWorkEventArgs e)
        {
            this.BW = sender as BackgroundWorker;

            if (this.ExcelInfo == null)
                throw new ArgumentNullException("ExcelWorkSpaceInfo is null");

            List<PictureItem> items;
            using (UsingExcel xls = new UsingExcel())
            {
                items = xls.GetPhotoItems(ExcelInfo, BW);
            }
            if (items == null || items.Count == 0)
                throw new Exception("Нет записей в списке");

            BW.ReportProgress(-1, $"Записей: {items.Count}.");

            Uploaders.Uploader up = new Uploaders.Uploader();
            up.UploadFolder = System.IO.Path.GetDirectoryName(ExcelInfo.WorkBook.Path) + @"\" + UploadFolderName;
            up.RunUpload(items, BW);

            using (UsingExcel xls = new UsingExcel())
            {
                xls.UpdatePhotoItems(items, ExcelInfo, BW);
            }

            e.Result = items.Count;
        }


        
    }
}
