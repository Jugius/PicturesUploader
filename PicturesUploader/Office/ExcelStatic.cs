using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PicturesUploader.Office
{
    static class ExcelStatic
    {
        public static bool IsExcelAppInstalled()
        {
            Type officeType = Type.GetTypeFromProgID("Excel.Application");
            if (officeType == null) return false;
            else return true;
        }
        public static string OpenExcelFileDialog()
        {
            OpenFileDialog f = new OpenFileDialog();
            f.Filter = "Файлы Excel|*.xls;*.xlsx;*.xlsm";
            f.Title = "Выберите файл";
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
