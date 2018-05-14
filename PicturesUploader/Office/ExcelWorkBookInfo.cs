using System.Collections.Generic;


namespace PicturesUploader.Office
{
    class ExcelWorkBookInfo
    {
        public string Path { get; private set; }
        public List<ExcelSheet> Sheets { get; set; } = new List<ExcelSheet>();
        public ExcelWorkBookInfo(string filePath)
        {
            this.Path = filePath;
        }
    }
}
