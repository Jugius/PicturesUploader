using System;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PicturesUploader.Office
{
    class UsingExcel : IDisposable
    {
        private Excel.Application xlApp = null;
        private System.Globalization.CultureInfo oldCultureInfo;

        public ExcelWorkBookInfo ReadExcelFileInfo(string path)
        {
            try
            {
                Excel.Workbook xlWorkBook = OpenExcelFile(path);
                Excel.Sheets xlWorkSheets = xlWorkBook.Sheets;

                ExcelWorkBookInfo result = new ExcelWorkBookInfo(path);

                foreach (Excel.Worksheet sheet in xlWorkSheets)
                {
                    ExcelSheet s = GetExcelSheetInfo(sheet);
                    result.Sheets.Add(s);
                }

                xlWorkBook.Close();
                Release(xlWorkSheets);
                Release(xlWorkBook);
                xlApp.DisplayAlerts = true;
                xlApp.Quit();

                return result;

            }
            catch (Exception ex)
            {
                try
                {
                    if (xlApp != null)
                        ExitExcelApplication();
                }
                finally { throw ex; }
            }

        }
        private ExcelSheet GetExcelSheetInfo(Excel.Worksheet sheet)
        {
            int RowsUsed = -1;
            int ColsUsed = -1;
            ExcelSheet result = new Office.ExcelSheet();
            result.Name = sheet.Name;
            result.Index = sheet.Index;
            Excel.Range workRange = sheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell);

            RowsUsed = workRange.Row;
            ColsUsed = workRange.Column;
            Release(workRange);

            result.LastCell = new ExcelLastCell(RowsUsed, ColsUsed);

            Release(workRange);

            return result;
        }
        private Excel.Workbook OpenExcelFile(string filePath)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            xlApp = new Excel.Application();

            xlApp.UserControl = false;
            xlApp.ScreenUpdating = false;
            xlApp.EnableEvents = false;
            xlApp.DisplayAlerts = false;
            xlApp.Visible = false;

            Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(filePath);

            return xlWorkBook;
        }
        private void ExitExcelApplication()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCultureInfo;
            xlApp.UserControl = true;
            xlApp.ScreenUpdating = true;
            xlApp.EnableEvents = true;
            xlApp.DisplayAlerts = true;
            xlApp.Quit();
        }

        #region Disposing Logic
        private bool iDisposed = false;
        public void Dispose()
        {
            if (!iDisposed)
            {
                iDisposed = true;
                ReleaseUnmanaged();
                GC.SuppressFinalize(this);
            }

        }
        private void ReleaseUnmanaged()
        {
            Release(xlApp);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();

        }
        private void Release(object sender)
        {
            try
            {
                if (sender != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(sender);
                    sender = null;
                }
            }
            catch (Exception) { sender = null; }
        }
        #endregion
    }
}
