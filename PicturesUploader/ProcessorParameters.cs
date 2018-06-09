using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicturesUploader
{
    class ProcessorParameters
    {
        public Office.ExcelWorkSpaceInfo ExcelInfo { get; set; }
        public Uploaders.UploadPicturesDirection Direction { get; set; }
        public string UploadFolderName { get; set; }
    }
}
