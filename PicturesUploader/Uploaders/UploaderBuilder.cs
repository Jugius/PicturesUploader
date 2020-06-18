using PicturesUploader.FTPConnection;
using System;

namespace PicturesUploader.Uploaders
{
    class UploaderBuilder
    {
        public UploaderBuilder(string directoryName)
        {
            this.DirectoryName = directoryName;
        }
        string DirectoryName;

        public IUploader Build(UploadDirection direction)
        {
            if (direction == UploadDirection.LOCAL)
                return new UploaderLocal(this.DirectoryName);
            else if (direction == UploadDirection.FTP)
            {
                FTPConnectionSettings ftp = FTPConnectionSettings.LoadSettings();
                if (ftp == null)
                    throw new Exception("Не указаны параметры подключения к FTP");

                return new UploaderFTP(ftp, this.DirectoryName);
            }
            else
            {
                throw new Exception("Ошибка создания загрузчика.");
            }
        }

    }
}
