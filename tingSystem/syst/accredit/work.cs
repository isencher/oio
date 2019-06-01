using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.model;
using System.Data.Entity;

namespace ting.pal.accredit
{
    public class work : generalwork<sysContext, Accredit>
    {
        public work()
        {
            editDialog = new dialog();
            queryDialog = null;
            ValidateBeforeDelete = null;
            CustomGetAll += () =>
            {
                using (var sc = new sysContext())
                {
                    return sc.Accredits
                        .Include(a => a.Right.Role)
                        .Include(a => a.Right.SetofBook)
                        .Include(a => a.Right.LevelTwo.LevelOne.Module)
                        .Include(a => a.Operation).ToList();
                }
            };
        }
    }
}
