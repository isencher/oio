using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.cash.model;
using ting.dal;
using ting.pal;

namespace ting.cash.standingbook
{
    public class work : generalwork<cashContext, StandingBook>
    {
        public work()
        {
            editDialog = new dialog();
            CustomGetAll += () =>
            {
                var all = repo.GetAllwithInclude<cashContext, StandingBook>()
                                .OrderByDescending(a=>a.Id);
                return all.ToList();
            };
        }
    }
}
