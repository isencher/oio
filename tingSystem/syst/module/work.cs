using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.dal;
using ting.model;

namespace ting.pal.module
{
    public class work : generalwork<sysContext, Module>
    {
        public work()
        {
            //ColumnConfigXML = "syst.xml";
            editDialog = new dialog();
            queryDialog = new query();
            ValidateBeforeDelete += (entity) =>
            {
                var ds = repo.GetAll<sysContext,LevelOne>().Where(o => o.ModuleId == entity.Id).ToList();
                if (ds.Count > 0) { return false; } else { return true; }
            };
        }
    }
}
