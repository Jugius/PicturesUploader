using PicturesUploader.FTPConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicturesUploader.Uploaders
{
    class UploaderFTP : Uploader
    {
        public FTPConnectionSettings FTPSettings { get; set; }
    }
}
