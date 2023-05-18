using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PicturesUploader.Office
{
    public static class ExcelStatic
    {
        public static bool IsExcelAppInstalled()
        {
            Type officeType = Type.GetTypeFromProgID("Excel.Application");
            if (officeType == null) return false;
            else return true;
        }
        public static string OpenExcelFileDialog()
        {
            OpenFileDialog f = new OpenFileDialog
            {
                Filter = "Файлы Excel|*.xlsx;*.xlsm",
                Title = "Выберите файл"
            };
            if (f.ShowDialog() == DialogResult.OK)
                return f.FileName;
            return null;
        }
        public static string GetColumnName(int columnNumber)
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
        public static int GetColumnNumber(string colAdress)
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
        public static List<string> GetColumnNames(int columnNumber)
        {
            List<string> columns = new List<string>();
            for (int i = 1; i <= columnNumber; i++)
            {
                columns.Add(GetColumnName(i));
            }
            return columns;
        }
    }
}
