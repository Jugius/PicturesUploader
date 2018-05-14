namespace PicturesUploader
{
    public class PictureItem
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; } = false;
        public PictureItem(string name, string address)
        {
            this.Name = name;
            this.Address = address;
        }
        public PictureItem()
        {
        }
    }
}
