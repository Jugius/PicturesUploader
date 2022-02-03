using System.Linq;

namespace PicturesUploader.Office
{
    class UploadingExcelParameters
    {
        internal string FilePath { get; }
        public int SheetIndex { get; }        
        public string ColumnWithNames { get; }
        public string ColumnWithLinks { get; }
        public int RowBegin { get; }
        public int RowEnd { get; }
        public bool IncludeLinkToCell { get; set; }
        public UploadingExcelParameters(string filePath, int sheetIndex, string columnWithNames, string columnWithLinks, int rowBegin, int rowEnd)
        {
            this.FilePath = filePath;
            this.SheetIndex = sheetIndex;
            this.ColumnWithNames = columnWithNames;
            this.ColumnWithLinks = columnWithLinks;
            this.RowBegin = rowBegin;
            this.RowEnd = rowEnd;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (object.ReferenceEquals(this, obj)) return true;

            UploadingExcelParameters other = obj as UploadingExcelParameters;
            if (other == null) return false;

            return this.FilePath == other.FilePath &&
                    this.SheetIndex == other.SheetIndex &&
                    this.ColumnWithNames == other.ColumnWithNames &&
                    this.ColumnWithLinks == other.ColumnWithLinks &&
                    this.RowBegin == other.RowBegin &&
                    this.RowEnd == other.RowEnd;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
