using PicturesUploader.FTPConnection;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PicturesUploader
{
    public partial class QuickLoadDialog : Form
    {        
        public QuickLoadDialog()
        {
            InitializeComponent();
            pictureBox1.AllowDrop = true;
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();            
            f.Filter = "Графические файлы|*.jpg;*.jpeg;*.gif;*.png";
            f.Title = "Выберите файл";
            if (f.ShowDialog() == DialogResult.OK)
                LoadFile(f.FileName);
        }
        private void LoadFile(object file)
        {
            BackgroundWorker fileLoader = new BackgroundWorker();
            fileLoader.DoWork += FileLoader_DoWork;
            fileLoader.RunWorkerCompleted += FileLoader_RunWorkerCompleted;
            fileLoader.RunWorkerAsync(file);
        }
        private void FileLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            ImageResizer.ImageInfo image = ImageResizer.ImageInfo.Build(e.Argument);
            ImageResizer.ResizeSettings settings = new ImageResizer.ResizeSettings();
            settings.ScaleMode = ImageResizer.ScaleMode.None;
            image.ResizeImage(settings);
            e.Result = image;
        }
        private void FileLoader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                MessageBox.Show(e.Error.Message, "Ошибка чтения файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                BackgroundWorker fileUploader = new BackgroundWorker();
                fileUploader.RunWorkerCompleted += FileUploader_RunWorkerCompleted;
                fileUploader.DoWork += FileUploader_DoWork;
                fileUploader.RunWorkerAsync(e.Result);
            }
        }

        private void FileUploader_DoWork(object sender, DoWorkEventArgs e)
        {
            FTPConnectionSettings ftp = FTPConnectionSettings.LoadSettings();
            if (ftp == null)
                throw new Exception("Не указаны параметры подключения к FTP");

            Uploaders.UploaderFTP uploader = new Uploaders.UploaderFTP(ftp, "quickUploads");
            uploader.Initialize();

            ImageResizer.ImageInfo image = e.Argument as ImageResizer.ImageInfo;

            string pictureName = $"{Guid.NewGuid()}.{image.SourceExtention}";
            Uri result = uploader.SaveImage(image.DestinationBitmap, pictureName);

            e.Result = result;
        }

        private void FileUploader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
                MessageBox.Show(e.Error.Message, "Ошибка загрузки файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                Uri uri = e.Result as Uri;
                pictureBox1.LoadAsync(uri.AbsoluteUri);
                label1.Visible = false;
                lblFileName.Text = uri.AbsoluteUri;
                Clipboard.SetText(uri.AbsoluteUri);
            }
        }

        private void btnCopyLink_Click(object sender, EventArgs e)
        {
            if (lblFileName.Text != "Ссылка на файл")
                Clipboard.SetText(lblFileName.Text);
        }

        private void pbPhoto_DragDrop(object sender, DragEventArgs e)
        {
            LoadDroppedBitmap(e);
        }
        private void pbPhoto_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) || e.Data.GetDataPresent("FileGroupDescriptor"))
            {
                e.Effect = DragDropEffects.Copy;
                return;
            }
            foreach (string s in e.Data.GetFormats())
            {
                if (s.Contains("UniformResourceLocator"))
                {
                    e.Effect = DragDropEffects.Copy;
                    break;
                }
            }
            //else
            //    e.Effect = DragDropEffects.None;
        }
        private void LoadDroppedBitmap(DragEventArgs e)
        {
            // перетягивание файла из папки
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                string soursePath = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                LoadFile(soursePath);
            }
            // перетягивание файла из приложения типа Outlook
            else if (e.Data.GetDataPresent("FileGroupDescriptor"))
            {
                System.IO.MemoryStream ms = (System.IO.MemoryStream)e.Data.GetData("FileContents", true);
                LoadFile(ms);
            }
            else if (e.Data.GetDataPresent(DataFormats.Text))
            {
                string soursePath = e.Data.GetData(DataFormats.StringFormat).ToString();
                
                int newlinePosition = soursePath.IndexOf('\n');
                if (newlinePosition >= 0)
                    soursePath = soursePath.Substring(0, newlinePosition);

                LoadFile(soursePath);
            }
            else
                MessageBox.Show("Не найден источник файла", "Ошибка чтения файла", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnPasteImage_Click(object sender, EventArgs e)
        {
            var image = Clipboard.GetImage();
            if (image != null)
                LoadFile(image);
        }
    }
}
