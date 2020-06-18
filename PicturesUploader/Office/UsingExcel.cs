using System;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace PicturesUploader.Office
{
    internal class UsingExcel : IExcelProcessor, IDisposable
    {
        private string OpenedExcelFile;
        public ExcelFileInfo ReadExcelFileInfo(string fileName)
        {
            try
            {
                InitializeExcelApplication();
                this.xlWorkBook = IExcelProcessor.OpenWorkbook(this.xlApp, fileName, true);

                List<ExcelSheet> sheets = new List<ExcelSheet>();
                Excel.Sheets xlWorkSheets = xlWorkBook.Sheets;

                foreach (Excel.Worksheet sheet in xlWorkSheets)
                    sheets.Add(GetSheetInfo(sheet));

                ExcelFileInfo result = new ExcelFileInfo(fileName, sheets);

                xlWorkBook.Close();
                ExitExcelApplication();
                return result;
            }
            catch
            {
                if (xlWorkBook != null)
                    xlWorkBook.Close();

                if (xlApp != null)
                    ExitExcelApplication();

                throw;
            }
        }
        public List<PictureItem> GetPhotoItems(UploadingExcelParameters excelInfo)
        {
            this.OpenedExcelFile = excelInfo.FilePath;
            try
            {
                InitializeExcelApplication();
                this.xlWorkBook = IExcelProcessor.OpenWorkbook(this.xlApp, excelInfo.FilePath);
                this.xlWorkSheet = (Excel.Worksheet)xlWorkBook.Sheets[excelInfo.SheetIndex];

                List<PictureItem> Items = new List<PictureItem>();
                for (int i = excelInfo.RowBegin; i <= excelInfo.RowEnd; i++)
                {
                    string picName = null;
                    if (!string.IsNullOrEmpty(excelInfo.ColumnWithNames))
                    {
                        if (xlWorkSheet.Range[excelInfo.ColumnWithNames + i].Value != null)
                        {
                            picName = xlWorkSheet.Range[excelInfo.ColumnWithNames + i].Value.ToString().Trim();
                        }
                    }

                    if (string.IsNullOrEmpty(picName))
                        picName = Guid.NewGuid().ToString();

                    PictureItem item = new PicturesUploader.PictureItem(i, picName);
                    Uri url = GetUrlFromCell(xlWorkSheet.Range[excelInfo.ColumnWithLinks + i]);
                    
                    if (url == null)
                    {
                        item.Error = new Exception("Не найдена ссылка в ячейке");
                    }
                    else
                    {
                        item.Address = url;
                    }

                    Items.Add(item);
                }

                return Items;
            }
            finally
            {
                xlWorkBook.Close();
                ExitExcelApplication();
            }
        }   
        public void UpdatePhotoItems(IEnumerable<PictureItem> items, UploadingExcelParameters excelInfo)
        {
            try
            {
                InitializeExcelApplication();
                this.xlWorkBook = IExcelProcessor.OpenWorkbook(this.xlApp, excelInfo.FilePath);
                this.xlWorkSheet = (Excel.Worksheet)xlWorkBook.Sheets[excelInfo.SheetIndex];
                var sheetInfo = GetSheetInfo(this.xlWorkSheet);
                string targetColumn = ExcelStatic.GetColumnName(sheetInfo.LastCell.Column + 1);

                foreach (var item in items)
                {
                    if (item.Error == null)
                    {
                        string s = (item.Address.Scheme == Uri.UriSchemeFile) ? item.Address.LocalPath : item.Address.AbsoluteUri;
                        xlWorkSheet.Hyperlinks.Add(Anchor: xlWorkSheet.Range[targetColumn + item.RowIndex], Address: s, TextToDisplay: "Фото");
                    }
                    else
                        xlWorkSheet.Range[targetColumn + item.RowIndex].Value = item.Error.Message;
                }
                xlWorkBook.Save();
                xlWorkBook.Close();
                ExitExcelApplication();

            }
            catch
            {
                if (xlWorkBook != null)
                    xlWorkBook.Close();

                if (xlApp != null)
                    ExitExcelApplication();

                throw;
            }
        }
        private ExcelSheet GetSheetInfo(Excel.Worksheet sheet)
        {
            ExcelSheet result = new Office.ExcelSheet();
            result.Name = sheet.Name;
            result.Index = sheet.Index;
            Excel.Range workRange = sheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell);

            int RowsUsed = workRange.Row;
            int ColsUsed = workRange.Column;
            Release(workRange);

            result.LastCell = new ExcelLastCell(RowsUsed, ColsUsed);

            Release(workRange);

            return result;
        }
        private Uri GetUrlFromCell(Excel.Range range)
        {
            Uri u;            
            if (range == null)
                return null;

            if (range.Hyperlinks.Count > 0)
            {
                if (CreateUri(range.Hyperlinks[1].Address, out u))
                    return u;                
            }

            if (range.Value != null)
            {
                string url = range.Value.ToString().Trim();
                if (!string.IsNullOrWhiteSpace(url) && CreateUri(url, out u))
                    return u;               
            }

            if (range.Formula != null)
            {
                string formula = range.Formula.ToString();
                if (formula.Contains("HYPERLINK"))
                {
                    int Start, End;
                    Start = formula.IndexOf('"', 0) + 1;
                    End = formula.IndexOf('"', Start);
                    formula = formula.Substring(Start, End - Start);
                    if (CreateUri(formula, out u))
                        return u;
                }
            }            

            return null;
        }
        private bool CreateUri(string s, out Uri uri)
        {
            if (Uri.TryCreate(s, UriKind.Absolute, out uri))
                return true;

            string dir = System.IO.Path.GetDirectoryName(this.OpenedExcelFile);
            string path = System.IO.Path.Combine(dir, s);

            if (System.IO.File.Exists(path) && Uri.TryCreate(path, UriKind.Absolute, out uri))
                return true;

            return false;
        }      
    }
}
