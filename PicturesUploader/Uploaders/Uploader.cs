using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PicturesUploader.Uploaders
{
    class Uploader
    {        
        public string UploadFolder { get; set; }
        public virtual void UploadPictures(IEnumerable<PictureItem> items, BackgroundWorker bw)
        {
            if (!System.IO.Directory.Exists(UploadFolder))
            {
                System.IO.Directory.CreateDirectory(UploadFolder);
            }
            foreach (var item in items)
            {
                try {
                    byte[] imageByteArray = DownloadImageToByteArray(item.Address);
                    string ext = GetExtention(imageByteArray);
                    string pictureName = item.Name + "." + ext;
                    item.Address = UploadPicture(imageByteArray, pictureName);
                }
                catch(Exception ex)
                {
                    item.Address += $" Ошибка: {ex.Message}.";
                }                
            }
        }

        private string UploadPicture(byte[] imageByteArray, string pictureName)
        {
            string destinationfile = UploadFolder + @"\" + pictureName;
            System.IO.File.WriteAllBytes(destinationfile, imageByteArray);
            return destinationfile;
        }

        protected string GetExtention(byte[] imageByteArray)
        {
            Image image = null;
            using (MemoryStream stream = new MemoryStream(imageByteArray))
            {
                image = Image.FromStream(stream);
            }

            if (ImageFormat.Jpeg.Equals(image.RawFormat))
            {
                return "jpg";
            }
            else if (ImageFormat.Png.Equals(image.RawFormat))
            {
                return "png";
            }
            else if (ImageFormat.Gif.Equals(image.RawFormat))
            {
                return "gif";
            }
            else if (ImageFormat.Tiff.Equals(image.RawFormat))
            {
                return "tif";
            }

            throw new Exception("Неизвестный формат");

        }

        protected byte[] DownloadImageToByteArray(string address)
        {
            if (!IsUrlImage(address))
                throw new Exception("Скачиваемый контент не является графическим изображением.");

            byte[] response;
            using (WebClient cl = new WebClient())
            {
                response = cl.DownloadData(address);
            }
            return response;
        }
        protected bool IsUrlImage(string url)
        {
            var request = WebRequest.Create(url);
            request.Timeout = 5000;
            using (var response = request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    if (!response.ContentType.Contains("text/html"))
                    {
                        using (var br = new BinaryReader(responseStream))
                        {
                            // e.g. test for a JPEG header here
                            var soi = br.ReadUInt16();  // Start of Image (SOI) marker (FFD8)
                            var jfif = br.ReadUInt16(); // JFIF marker (FFE0)
                            return soi == 0xd8ff && jfif == 0xe0ff;
                        }
                    }
                }
            }
            return false;
        }
    }
}
