using System;
using System.Drawing;

namespace PicturesUploader.Uploaders
{
    public abstract class IUploader
    {
        protected string UploadFolder { get; }
        internal IUploader(string uploadFolder)
        {
            this.UploadFolder = uploadFolder;
        }
        internal abstract void Initialize();
        internal abstract Uri SaveImage(Image image, string imageName);
    }
}
