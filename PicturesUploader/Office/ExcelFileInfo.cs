using System.Collections.Generic;


namespace PicturesUploader.Office
{
    class ExcelFileInfo
    {
        internal string Path { get; }
        internal List<ExcelSheet> Sheets { get; }
        internal ExcelFileInfo(string filePath, List<ExcelSheet> sheets)
        {
            this.Path = filePath;
            this.Sheets = sheets;
        }
    }
}
