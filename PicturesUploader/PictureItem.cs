using System;

namespace PicturesUploader
{
    public class PictureItem
    {
        public string Name { get; }
        public int RowIndex { get; }
        public Uri Address { get; set; }
        public Exception Error { get; set; }
        public PictureItem(int rowIndex, string name)
        {
            this.RowIndex = rowIndex;
            this.Name = name;
        }
        public PictureItem(int rowIndex, string name, Uri address) : this(rowIndex, name)
        {
            this.Address = address;
        }       
    }
}
