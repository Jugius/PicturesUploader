using PicturesUploader.Office;


namespace PicturesUploader.Uploaders
{
    abstract class UploaderParameters
    {
        public string FilePath { get; private set; }
        public string PictureNamesColumn { get; set; }
        public string PictureHyperlinksColumn { get; set; }
        public int RowBeginUpoload { get; set; }
        public int RowEndUpload { get; set; }
        public ExcelSheet SelectedSheet { get; set; }
    }
}
