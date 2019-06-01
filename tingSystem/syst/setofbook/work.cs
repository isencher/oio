using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.dal;
using ting.model;

namespace ting.pal.setofbook
{
    public class work : generalwork<sysContext, SetofBook>
    {
        public work()
        {
            editDialog = new dialog();
            queryDialog = null;
            ValidateBeforeDelete += (entity) =>
            {
                var ds = repo.GetAll<sysContext,Right>().Where(o => o.SetofBookId == entity.Id).ToList();
                if (ds.Count > 0) { return false; } else { return true; }
            };
        }
    }

}
