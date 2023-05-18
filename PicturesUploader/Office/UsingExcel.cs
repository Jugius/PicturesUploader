using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

namespace PicturesUploader.Office
{
    internal class UsingExcel
    {
        private static readonly HashSet<string> allowedFileExtentions = 
            new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".xlsx", ".xlsm" };

        private string OpenedExcelFile;
        public ExcelFileInfo ReadExcelFileInfo(string fileName)
        {
            var file = new FileInfo(fileName);

            if (!allowedFileExtentions.Contains(file.Extension))
                throw new Exception("Неподдерживаемый формат. Поддерживаются только файлы Office 2007 и выше, формата .xlsx и .xlsm");

            ExcelFileInfo result;
            using (var fileStream = file.OpenRead())
            {
                using (ExcelPackage excel = new ExcelPackage(fileStream))
                {
                    if (excel.Workbook?.Worksheets?.Count == 0)
                        throw new Exception("Не найдены листыы в файле!");

                    List<ExcelSheet> sheets = new List<ExcelSheet>();
                    foreach (var sheet in excel.Workbook.Worksheets)
                    {
                        sheets.Add(GetSheetInfo(sheet));
                    }
                    result = new ExcelFileInfo(fileName, sheets);
                }
            }
            return result;
        }       

        public List<PictureItem> GetPhotoItems(UploadingExcelParameters excelInfo)
        {
            this.OpenedExcelFile = excelInfo.FilePath;
            var file = new FileInfo(excelInfo.FilePath);

            List<PictureItem> items = new List<PictureItem>(excelInfo.RowEnd - excelInfo.RowBegin + 1);
            int picNamesColumnNumber = string.IsNullOrEmpty(excelInfo.ColumnWithNames) ? 0 : ExcelStatic.GetColumnNumber(excelInfo.ColumnWithNames);
            int picUrlColumnNumber = ExcelStatic.GetColumnNumber(excelInfo.ColumnWithLinks);

            using (var fileStream = file.OpenRead())
            {
                using (ExcelPackage excel = new ExcelPackage(fileStream))
                {
                    var sheet = excel.Workbook.Worksheets[excelInfo.SheetIndex];
                    
                    for (int row = excelInfo.RowBegin; row <= excelInfo.RowEnd; row++)
                    {
                        string picName = null;
                        if (picNamesColumnNumber > 0)
                        {
                            picName = sheet.Cells[row, picNamesColumnNumber].GetValue<string>();                            
                        }

                        if (string.IsNullOrWhiteSpace(picName))
                        {
                            picName = Guid.NewGuid().ToString();
                        }
                        else
                        { 
                            picName = picName.Trim();
                        }

                        PictureItem item = new PicturesUploader.PictureItem(row, picName);
                        Uri url = GetUrlFromCell(sheet.Cells[row, picUrlColumnNumber]);

                        if (url == null)
                        {
                            item.Error = new Exception("Не найдена ссылка в ячейке");
                        }
                        else
                        {
                            item.Address = url;
                        }

                        items.Add(item);
                    }
                }
            }
            return items;
        }
        public void UpdatePhotoItems(IEnumerable<PictureItem> items, UploadingExcelParameters excelInfo)
        {
            using (ExcelPackage excel = new ExcelPackage(excelInfo.FilePath))
            {
                var sheet = excel.Workbook.Worksheets[excelInfo.SheetIndex];
                var sheetInfo = GetSheetInfo(sheet);
                int targetColumn = sheetInfo.LastCell.Column + 1;

                foreach (var item in items)
                {
                    var cell = sheet.Cells[item.RowIndex, targetColumn];

                    if (item.Error == null)
                    {
                        string link = (item.Address.Scheme == Uri.UriSchemeFile) ? item.Address.LocalPath : item.Address.AbsoluteUri;
                        string val = excelInfo.IncludeLinkToCell ? link : "Фото";
                        cell.Hyperlink = new Uri(link);
                        cell.Value = val;
                    }
                    else
                    {
                        cell.Value = item.Error.Message;
                    }                        
                }
                excel.Save();
            }            
        }
        private static ExcelSheet GetSheetInfo(ExcelWorksheet sheet)
        {
            ExcelSheet result = new Office.ExcelSheet
            {
                Name = sheet.Name,
                Index = sheet.Index,
            };

            if (sheet.Dimension == null)
            {
                result.LastCell = new ExcelLastCell(1, 1);
            }
            else
            {
                result.LastCell = new ExcelLastCell(sheet.Dimension.End.Row, sheet.Dimension.End.Column);
            }

            return result;
        }

        private Uri GetUrlFromCell(ExcelRange cell)
        {
            if (cell == null)
                return null;

            if (cell.Hyperlink != null)
            {
                if (CreateUri(cell.Hyperlink.OriginalString, out Uri u))
                    return u;
            }

            if (cell.Value != null)
            {
                string url = cell.GetValue<string>().Trim();
                if (!string.IsNullOrWhiteSpace(url) && Uri.TryCreate(url, UriKind.Absolute, out Uri u))
                    return u;
            }

            if (cell.Formula != null)
            {
                string formula = cell.Formula;
                if (formula.Contains("HYPERLINK"))
                {
                    int Start, End;
                    Start = formula.IndexOf('"', 0) + 1;
                    End = formula.IndexOf('"', Start);
                    formula = formula.Substring(Start, End - Start);
                    string unescaped = Uri.UnescapeDataString(formula);
                    if (CreateUri(unescaped, out Uri u))
                        return u;
                }
            }
            return null;
        }
        //private Uri GetUrlFromCell(Excel.Range range)
        //{
        //    Uri u;            
        //    if (range == null)
        //        return null;

        //    if (range.Hyperlinks.Count > 0)
        //    {
        //        if (CreateUri(range.Hyperlinks[1].Address, out u))
        //            return u;                
        //    }

        //    if (range.Value != null)
        //    {
        //        string url = range.Value.ToString().Trim();
        //        if (!string.IsNullOrWhiteSpace(url) && CreateUri(url, out u))
        //            return u;               
        //    }

        //    if (range.Formula != null)
        //    {
        //        string formula = range.Formula.ToString();
        //        if (formula.Contains("HYPERLINK"))
        //        {
        //            int Start, End;
        //            Start = formula.IndexOf('"', 0) + 1;
        //            End = formula.IndexOf('"', Start);
        //            formula = formula.Substring(Start, End - Start);
        //            if (CreateUri(formula, out u))
        //                return u;
        //        }
        //    }            

        //    return null;
        //}
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
