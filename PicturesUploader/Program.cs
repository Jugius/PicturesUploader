using System;
using System.Reflection;
using System.Windows.Forms;

namespace PicturesUploader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            UpgradeIfRequired();

            if (!Office.ExcelStatic.IsExcelAppInstalled())
            {
                MessageBox.Show("Microsoft Excel не найден на этом компьютере", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            EmbeddedAssembly.Load("PicturesUploader.office.dll", "office.dll");
            EmbeddedAssembly.Load("PicturesUploader.Microsoft.Office.Interop.Excel.dll", "Microsoft.Office.Interop.Excel.dll");
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return EmbeddedAssembly.Get(args.Name);
        }
        static void UpgradeIfRequired()
        {
            if (Properties.Settings.Default.SettingsUpgradeRequired)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.SettingsUpgradeRequired = false;
                Properties.Settings.Default.Save();
            }
        }
    }
}
