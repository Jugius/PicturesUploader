using OfficeOpenXml;
using System;

namespace PicturesUploader.Office
{
    internal class ExcelUriBuilder
    {
        private readonly string fileDirectory;

        public ExcelUriBuilder(string excelFilePath)
        {
            this.fileDirectory = System.IO.Path.GetDirectoryName(excelFilePath);
        }
        private bool TryCreate(string s, out Uri uri)
        {
            if (Uri.TryCreate(s, UriKind.Absolute, out uri))
                return true;

            string path = System.IO.Path.Combine(this.fileDirectory, s);

            if (System.IO.File.Exists(path) && Uri.TryCreate(path, UriKind.Absolute, out uri))
                return true;

            return false;
        }
        public Uri GetUrlFromCell(ExcelRange cell)
        {
            if (cell == null)
                return null;

            if (cell.Hyperlink != null)
            {
                if (TryCreate(cell.Hyperlink.OriginalString, out Uri u))
                    return u;
            }

            if (cell.Value != null)
            {
                string url = cell.GetValue<string>().Trim();
                if (!string.IsNullOrWhiteSpace(url) && TryCreate(url, out Uri u))
                    return u;
            }

            if (cell.Formula != null)
            {
                string formula = cell.Formula;
                if (formula.Contains("HYPERLINK"))
                {
                    int Start, End;
                    Start = formula.IndexOf('"', 0) + 1;
                    End = formula.IndexOf('"', Start);
                    formula = formula.Substring(Start, End - Start);
                    string unescaped = Uri.UnescapeDataString(formula);
                    if (TryCreate(unescaped, out Uri u))
                        return u;
                }
            }
            return null;
        }
    }
}
