using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ting.pal
{
    public class ColumnInfo
    {
        public string Table { get; set; }
        public string Name { get; set; }
        public string DataPropertyName { get; set; }
        public string HeaderText { get; set; }
        public string Type { get; set; }
        public int Width { get; set; }
        public string Alignment { get; set; }
        public string Format { get; set; }

    }
}
