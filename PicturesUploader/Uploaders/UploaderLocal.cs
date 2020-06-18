using System;
using System.Drawing;

namespace PicturesUploader.Uploaders
{
    class UploaderLocal : IUploader
    {
        internal UploaderLocal(string uploadFolder) : base(uploadFolder) { }
        internal override void Initialize()
        {
            if (!System.IO.Directory.Exists(UploadFolder))
            {
                System.IO.Directory.CreateDirectory(UploadFolder);
            }
        }
        internal override Uri SaveImage(Image image, string imageName)
        {
            using (ImageResizer.ImageInfo imageI = ImageResizer.ImageInfo.Build(image))
            {
                string destinationfile = UploadFolder + @"\" + imageName;
                imageI.SaveAs(destinationfile);
                return new Uri(destinationfile);
            }
            //ImageConverter _imageConverter = new ImageConverter();
            //byte[] imageByteArray = (byte[])_imageConverter.ConvertTo(image, typeof(byte[]));
            //string destinationfile = UploadFolder + @"\" + imageName;
            //image.Save(destinationfile);//, System.Drawing.Imaging.ImageFormat.Jpeg);
            //System.IO.File.WriteAllBytes(destinationfile, imageByteArray);
            
        }
    }
}
