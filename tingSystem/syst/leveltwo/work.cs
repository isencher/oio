using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.dal;
using ting.model;

namespace ting.pal.leveltwo
{
    public class work : generalwork<sysContext, LevelTwo>
    {
        public work()
        {
            editDialog = new dialog();
            queryDialog = null;
            ValidateBeforeDelete += (entity) =>
            {
                var ds = repo.GetAll<sysContext,Right>().Where(o => o.LevelTwoId == entity.Id).ToList();
                if (ds.Count > 0) { return false; } else { return true; }
            };
            CustomGetAll += () =>
            {
                using (var sc = new sysContext())
                {
                    return sc.LevelTwos.Include("LevelOne").ToList();
                }
            };
        }
    }
}
