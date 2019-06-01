using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.cash.model;
using ting.pal;

namespace ting.cash.account
{
    public class work : generalwork<cashContext , Account>
    {
        public work()
        {
            editDialog = new dialog();
        }
    }
}
