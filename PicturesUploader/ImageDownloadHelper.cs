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

            Uri uri = new Uri(url);
            if (uri.HasRequestProtection())
            {
                do
                {
                    try
                    {
                        return IsUrlImage(url);
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
                return IsUrlImage(url);
            }
        }

        private static bool IsUrlImage(Uri uri)
        {
            var req = (HttpWebRequest)HttpWebRequest.Create(uri);
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
            Uri uri = new Uri(address);

            if (uri.HasRequestProtection())
            {
                do
                {
                    try
                    {
                        return DownloadData(uri);
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
                return DownloadData(uri);
            }


        }
        public static byte[] DownloadData(Uri uri)
        {
            byte[] response;
            using (WebClient cl = new WebClient())
            {
                response = cl.DownloadData(uri);
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
        public static bool HasRequestProtection(this Uri uri)
        {
            if (uri.Host == "maps.googleapis.com")
                return true;
            return false;
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
