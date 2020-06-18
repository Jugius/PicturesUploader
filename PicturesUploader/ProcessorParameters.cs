using PicturesUploader.Uploaders;
using System.Linq;

namespace PicturesUploader
{
    class ProcessorParameters
    {
        public Office.UploadingExcelParameters ExcelInfo { get; }        
        public ImageResizer.ResizeSettings ImageResizeSettings { get; }
        public UploadDirection UploadDirection { get; set; }
        public string UploadDirectory { get; set; }
        internal ProcessorParameters(Office.UploadingExcelParameters excelInfo)
        {
            this.ExcelInfo = excelInfo;
            this.ImageResizeSettings = new ImageResizer.ResizeSettings();
            if (PicturesUploader.Properties.Settings.Default.NewImageSize == 0)
            {
                this.ImageResizeSettings.ScaleMode = ImageResizer.ScaleMode.None;
            }
            else
            {
                this.ImageResizeSettings.ScaleMode = ImageResizer.ScaleMode.DownscaleOnly;
                this.ImageResizeSettings.ResizeMode = ImageResizer.ResizeMode.MaxSides;
                var size = ImageResizer.ImageSize.GetDefaults().FirstOrDefault(a => a.ID == PicturesUploader.Properties.Settings.Default.NewImageSize);
                this.ImageResizeSettings.Height = size.Size.Height;
                this.ImageResizeSettings.Width = size.Size.Width;
            }            
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (object.ReferenceEquals(this, obj)) return true;

            ProcessorParameters other = obj as ProcessorParameters;
            if (other == null) return false;

            return this.ExcelInfo.Equals(other.ExcelInfo) &&
                    this.UploadDirection == other.UploadDirection &&
                    this.UploadDirectory == other.UploadDirectory &&
                    this.ImageResizeSettings.Equals(other.ImageResizeSettings);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
