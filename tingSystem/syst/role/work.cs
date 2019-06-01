using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.model;

namespace ting.pal.role
{
    public class work : generalwork<sysContext, Role>
    {
        public work()
        {
            //ColumnConfigXML = "syst.xml";
            editDialog = new dialog();
            queryDialog = null;
            ValidateBeforeDelete += (entity) =>
            {
                try
                {
                    using (var sc = new sysContext())
                    {
                        var ds = sc.Users.Include("Partners").Where(o => o.Partners.Select(r => r.Id).Contains(entity.Id)).ToList();
                        if (ds.Count > 0) { return false; } else { return true; }
                    }
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            };
            CustomGetAll = null;
        }
    }
}
