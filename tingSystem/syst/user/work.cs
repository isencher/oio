using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using ting.model;
using ting.dal;

namespace ting.pal.user
{
    public class work : generalwork<sysContext, User>
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
                        var rs = sc.Users.Include(u => u.Partners)
                        .FirstOrDefault(u => u.Id == entity.Id).Partners;
                        var ds = sc.Rights.Where(o => rs.Select(r => r.Id).Contains(o.RoleId)).ToList();
                        if (ds.Count > 0) { return false; } else { return true; }
                    }
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            };
            CustomGetAll += () =>
            {
                using (var sc = new sysContext())
                {
                    return sc.Users.Include("Partners").OrderByDescending(u=>u.Id).ToList();
                }
            };
            CustomAdd += (entity) =>
            {
                return repo.m2mAdd<sysContext, User, Role>(entity);
            };
            CustomAlter += (entity) => 
            {
                return repo.m2mAlter<sysContext, User, Role>(entity);
            };
        }
    }
}
