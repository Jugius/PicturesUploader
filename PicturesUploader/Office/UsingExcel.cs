using System;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PicturesUploader.Uploaders;

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
                ExitExcelApplication();

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
        public List<PictureItem> GetPhotoItems(UploaderParameters parameters)
        {
            try
            {
                Excel.Workbook xlWorkBook = OpenExcelFile(parameters.FilePath);
                Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Sheets[parameters.SelectedSheet.Index];

                if (!CheckData(xlWorkSheet, parameters))
                {
                    xlWorkBook.Save();
                    xlWorkBook.Close();
                    Release(xlWorkSheet);
                    Release(xlWorkBook);
                    string error = "Обнаружены ошибки в таблице. Это могут быть пустые ячейки в столбце номеров плоскотей, либо пустые/некорректные гиперссылки" + Environment.NewLine +
                        "Ошибки выделены в файле. Исправьте и повторите";
                    throw new Exception(error);
                }

                List<PictureItem> Items = new List<PictureItem>();
                for (int i = parameters.RowBeginUpoload; i <= parameters.RowEndUpload; i++)
                {
                    Items.Add(new PictureItem(xlWorkSheet.Range[parameters.PictureNamesColumn + i].Value.ToString().Trim(),
                                            GetUrlFromCell(xlWorkSheet.Range[parameters.PictureHyperlinksColumn + i])));
                }

                xlWorkBook.Close();
                Release(xlWorkSheet);
                Release(xlWorkBook);
                ExitExcelApplication();
                return Items;
            }
            catch (Exception ex)
            {
                try
                {
                    if (xlApp != null)
                        ExitExcelApplication();
                }
                catch { }
                throw ex;
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
            oldCultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
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
        private bool CheckData(Excel.Worksheet sheet, UploaderParameters parameters)
        {
            int errors = 0;
            Excel.Range range = null;
            for (int i = parameters.RowBeginUpoload; i <= parameters.RowEndUpload; i++)
            {
                range = sheet.Range[parameters.PictureNamesColumn + i];
                if (range.Value == null || range.Value.ToString().Trim() == string.Empty)
                {
                    range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    errors++;
                }
                range = sheet.Range[parameters.PictureHyperlinksColumn + i];
                if (GetUrlFromCell(range) == null)
                {
                    range.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    errors++;
                }
            }
            Release(range);
            return (errors > 0) ? false : true;
        }
        private string GetUrlFromCell(Excel.Range range)
        {
            if (range == null)
                return null;

            if (range.Hyperlinks.Count > 0)
                return range.Hyperlinks[1].Address;

            if (range.Value == null)
                return null;

            string url = range.Value.ToString().Trim();
            if (string.IsNullOrEmpty(url))
                return null;

            Uri u;
            if (Uri.TryCreate(url, UriKind.Absolute, out u))
                return url;

            return null;
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
