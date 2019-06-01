using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.cash.model;
using ting.pal;

namespace ting.cash.unit
{
    public class work : generalwork<cashContext,Unit>
    {
        public work()
        {
            editDialog = new dialog();
        }
    }
}
