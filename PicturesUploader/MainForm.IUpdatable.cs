using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Updater;

namespace PicturesUploader
{
    partial class MainForm : IUpdatableApplication
    {
        public string ApplicationName
        {
            get
            {
                return "Pictures Uploader";
            }
        }

        public string ApplicationID
        {
            get
            {
                return "PicturesUploader";
            }
        }

        public Assembly ApplicationAssembly
        {
            get
            {
                return Assembly.GetExecutingAssembly();
            }
        }

        public Icon ApplicationIcon
        {
            get
            {
                return this.Icon;
            }
        }

        public Image ApplicationImage
        {
            get
            {
                return PicturesUploader.Properties.Resources.Hard_Disk_Server_icon;
            }
        }

        public Uri UpdateXmlLocation
        {
            get
            {
                return new Uri("http://oohelp.net/software/versions.xml");
            }
        }

        public Form Context
        {
            get
            {
                return this;
            }
        }

        public Version Version
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }
    }
}
