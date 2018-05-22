using System.Linq;

namespace PicturesUploader.Office
{
    class ExcelWorkSpaceInfo
    {
        public ExcelWorkBookInfo WorkBook { get; private set; }
        public string ColumnPictureNames { get; set; }
        public string ColumnPictureHyperlinks { get; set; }
        public int RowBeginUpload { get; set; }
        public int RowEndUpload { get; set; }
        public int SelectedSheetIndex { get; private set; }
        public ExcelSheet SelectedSheet { get { return this.WorkBook.Sheets.FirstOrDefault(a => a.Index == this.SelectedSheetIndex); } }
        public ExcelWorkSpaceInfo(ExcelWorkBookInfo workBook, int selectedSheetIndex)
        {
            this.WorkBook = workBook;
            this.SelectedSheetIndex = selectedSheetIndex;
        }
    }
}
