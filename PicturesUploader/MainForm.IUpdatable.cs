using SoftwareManagement.Updater;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace PicturesUploader
{
    partial class MainForm : IUpdatableApplication
    {
        private const string _name = "PicturesUploader";
        private const string _updatesServer = @"https://software.oohelp.net";
        public string ApplicationName => _name;
        public Form MainWindow => this;
        public Version Version => Assembly.GetExecutingAssembly().GetName().Version;
        public string UpdatesServerPath => _updatesServer;
    }
}
