using PicturesUploader.Office;
using PicturesUploader.Uploaders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            using (UsingExcel xls = new UsingExcel())
            {
                items = xls.GetPhotoItems(this.Parameters.ExcelInfo, this.BW);
            }
            if (items == null || items.Count == 0)
            {
                throw new Exception("Нет записей в списке");
            }

            UploaderBuilder builder = new Uploaders.UploaderBuilder();
            builder.RootFolder = System.IO.Path.GetDirectoryName(this.Parameters.ExcelInfo.WorkBook.Path);
            builder.UploadFolderName = this.Parameters.UploadFolderName;
            builder.UploadDirection = this.Parameters.Direction;

            var uploader = builder.Build();
            uploader.RunUpload(items, BW);

            using (UsingExcel xls = new UsingExcel())
            {
                xls.UpdatePhotoItems(items, this.Parameters.ExcelInfo, BW);
            }

            e.Result = items.Count;
        }


        
    }
}
