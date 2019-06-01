using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.dal;
using ting.model;

namespace ting.pal.operation
{
    public class work : generalwork<sysContext, Operation>
    {
        public work()
        {
            editDialog = new dialog();
            queryDialog = null;
            ValidateBeforeDelete += (entity) =>
            {
                var ds = repo.GetAll<sysContext,Accredit>().Where(o => o.OperationId == entity.Id).ToList();
                if (ds.Count > 0) { return false; } else { return true; }
            };
            CustomGetAll = null;
        }
    }
}
