using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.model;

namespace ting.pal.levelone
{
    public class work : generalwork<sysContext, LevelOne>
    {
        public work()
        {
            editDialog = new dialog();
            //queryDialog = new querylevelone();
            //ValidateBeforeDelete += (entity) =>
            //{
            //    return true;
            //};
            CustomGetAll += () =>
            {
                using (var sc = new sysContext())
                {
                    return sc.LevelOnes.Include("Module").ToList();
                }
            };
        }
    }
}
