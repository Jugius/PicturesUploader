using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PicturesUploader.Office
{
    public class ExcelLastCell
    {
        public int Row { get; private set; }
        public int Column { get; private set; }
        public ExcelLastCell(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }
    }
}
