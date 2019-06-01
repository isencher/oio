using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.dal;
using ting.model;
using System.Data.Entity;

namespace ting.pal.right
{
    public class work : generalwork<sysContext, Right>
    {
        public work()
        {
            editDialog = new dialog();
            queryDialog = null;
            ValidateBeforeDelete += (entity) =>
            {
                var ds = repo.GetAll<sysContext,Accredit>().Where(o => o.RightId == entity.Id).ToList();
                if (ds.Count > 0) { return false; } else { return true; }
            };
            CustomGetAll += () =>
            {
                using (var sc = new sysContext())
                {
                    var rights = sc.Rights.Include(p => p.Role).Include(p => p.SetofBook).Include(p => p.LevelTwo.LevelOne.Module).ToList();
                    return rights;
                }
            };
        }
    }
}
