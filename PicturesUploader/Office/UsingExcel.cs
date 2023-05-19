using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

namespace PicturesUploader.Office
{
    internal static class UsingExcel
    {
        private static readonly HashSet<string> allowedFileExtentions = 
            new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".xlsx", ".xlsm" };
                
        public static ExcelFileInfo ReadExcelFileInfo(string fileName)
        {
            if (!allowedFileExtentions.Contains(Path.GetExtension(fileName)))
                throw new Exception("Неподдерживаемый формат. Поддерживаются только файлы Office 2007 и выше, формата .xlsx и .xlsm");

            ExcelFileInfo result;

            using (ExcelPackage excel = new ExcelPackage(fileName))
            {
                if (excel.Workbook?.Worksheets?.Count == 0)
                    throw new Exception("Не найдены листы в файле!");

                List<ExcelSheet> sheets = new List<ExcelSheet>(excel.Workbook.Worksheets.Count);
                foreach (var sheet in excel.Workbook.Worksheets)
                {
                    sheets.Add(GetSheetInfo(sheet));
                }
                result = new ExcelFileInfo(fileName, sheets);
            }

            return result;
        }       

        public static List<PictureItem> GetPhotoItems(UploadingExcelParameters excelInfo)
        {
            List<PictureItem> items = new List<PictureItem>(excelInfo.RowEnd - excelInfo.RowBegin + 1);
            int picNamesColumnNumber = string.IsNullOrEmpty(excelInfo.ColumnWithNames) ? 0 : GetColumnNumber(excelInfo.ColumnWithNames);
            int picUrlColumnNumber = GetColumnNumber(excelInfo.ColumnWithLinks);

            using (ExcelPackage excel = new ExcelPackage(excelInfo.FilePath))
            {
                var sheet = excel.Workbook.Worksheets[excelInfo.SheetIndex];
                var uriBuilder = new ExcelUriBuilder(excelInfo.FilePath);

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
                    Uri url = uriBuilder.GetUrlFromCell(sheet.Cells[row, picUrlColumnNumber]);

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
            return items;
        }
        public static void UpdatePhotoItems(IEnumerable<PictureItem> items, UploadingExcelParameters excelInfo)
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
                try
                {
                    excel.Save();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Ошибка сохранения файла {excelInfo.FilePath}.", ex);
                }                
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
        private static string GetColumnAddress(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = string.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }
            return columnName;
        }
        private static int GetColumnNumber(string colAdress)
        {
            int[] digits = new int[colAdress.Length];
            for (int i = 0; i < colAdress.Length; ++i)
            {
                digits[i] = Convert.ToInt32(colAdress[i]) - 64;
            }
            int mul = 1; int res = 0;
            for (int pos = digits.Length - 1; pos >= 0; --pos)
            {
                res += digits[pos] * mul;
                mul *= 26;
            }
            return res;
        }
        public static List<string> GetColumnsNames(int columnNumber)
        {
            List<string> columns = new List<string>(columnNumber);
            for (int i = 1; i <= columnNumber; i++)
            {
                columns.Add(GetColumnAddress(i));
            }
            return columns;
        }
    }
}
