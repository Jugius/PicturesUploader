using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicturesUploader.Uploaders
{
    class UploaderBuilder
    {
        public UploadPicturesDirection UploadDirection { get; set; }
        public string RootFolder { get; set; }
        public string UploadFolderName { get; set; }

        public Uploader Build()
        {
            switch (UploadDirection)
            {
                case UploadPicturesDirection.FTP: return GetUploaderFTP();
                case UploadPicturesDirection.LOCAL:return GetUploader();
                default:return null;
            }
        }
        private Uploader GetUploader()
        {
            var uploader = new UploaderFTP();
            ////
            return uploader;
        }
        private UploaderFTP GetUploaderFTP()
        {
            var uploader = new UploaderFTP();
            ////
            return uploader;
        }
    }
}
