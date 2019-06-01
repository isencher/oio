using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ting.model;

namespace ting.pal
{
    public abstract class QueryDialog<T> : Form
        where T : class, IbaseProperties, new()
    {
        public List<T> DataSource { get; set; }
        public Action<List<T>> UpdateByFilter { get; set; }
    }
}
