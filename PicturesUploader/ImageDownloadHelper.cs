using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Net;

namespace PicturesUploader
{
    public static class ImageDownloadHelper
    {
        public static bool IsUrlImage(string url)
        {
            int attempts = 0;
                        
            if (url.HasRequestProtection())
            {
                do
                {
                    try
                    {
                        return IsUrlImageProtected(url);
                    }
                    catch (Exception ex)
                    {
                        if (!IsForbiddenStatusException(ex) || attempts == 5)
                            throw;

                        attempts++;
                        System.Threading.Thread.Sleep(1000);
                    }
                } while (attempts <= 5);
                return false;
            }
            else
            {
                return IsUrlImageProtected(url);
            }
        }

        private static bool IsUrlImageProtected(string url)
        {
            var req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "HEAD";
            req.Timeout = 5000;
            using (var resp = req.GetResponse())
            {
                return resp.ContentType.ToLower(CultureInfo.InvariantCulture)
                           .StartsWith("image/");
            }
        }
        public static byte[] DownloadData(string address)
        {
            int attempts = 0;

            if (address.HasRequestProtection())
            {
                do
                {
                    try
                    {
                        return DownloadDataProtected(address);
                    }
                    catch (Exception ex)
                    {
                        if (!IsForbiddenStatusException(ex) || attempts == 5)
                            throw;

                        attempts++;
                        System.Threading.Thread.Sleep(1000);
                    }
                } while (attempts <= 5);
                return null;
            }
            else
            {
                return DownloadDataProtected(address);
            }


        }
        private static byte[] DownloadDataProtected(string address)
        {
            byte[] response;
            using (WebClient cl = new WebClient())
            {
                response = cl.DownloadData(address);
            }
            return response;
        }


        private static bool IsForbiddenStatusException(Exception ex)
        {
            var wex = GetNestedException<WebException>(ex);

            if (wex == null) { return false; }

            var resp = wex.Response as HttpWebResponse;

            if (resp == null || resp.StatusCode != HttpStatusCode.Forbidden)
            {
                return false;
            }
            return true;
        }
        private static T GetNestedException<T>(Exception ex) where T : Exception
        {
            if (ex == null) { return null; }

            var tEx = ex as T;
            if (tEx != null) { return tEx; }

            return GetNestedException<T>(ex.InnerException);
        }
    }
    public static class Ext
    {
        public static bool HasRequestProtection(this string url)
        {
            return url.Contains("maps.googleapis.com");
        }
        public static string GetExtention(this byte[] imageByteArray)
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
    }
}
