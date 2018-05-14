using System.ComponentModel;

namespace PicturesUploader.Uploaders
{
    abstract class Uploader
    {
        protected BackgroundWorker BWorker;
        public Uploader(UploaderParameters parameters) { }
        public virtual void UploadFiles(object sender, DoWorkEventArgs e)
        {
            this.BWorker = sender as BackgroundWorker;
        }
    }
}
