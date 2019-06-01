using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.cash.model;
using ting.pal;

namespace ting.cash.category
{
    public class work : generalwork<cashContext, AccountCategory>
    {
        public work()
        {
            editDialog = new dialog();
        }
    }
}
