using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.model;

namespace ting.pal
{
    public class ClickActions<dbContext, T>
    where dbContext : DbContext, new()
    where T : class, IbaseProperties, new()

    {
        public void Add(generalwork<dbContext, T> work)
        {
            work.OpenAdd(new T());
        }
        public void Alter(generalwork<dbContext, T> work)
        {
            work.OpenAlter(work.Current);
        }
        public void Delete(generalwork<dbContext, T> work)
        {
            work.ToDelete(work.Current);
        }
        public void Deletes(generalwork<dbContext, T> work)
        {
            work.ToDeletes(work.DataSource);
        }
        public void Import(generalwork<dbContext, T> work)
        {
            work.ToImport();
        }
        public void Export(generalwork<dbContext, T> work)
        {
            work.ToExport();
        }
        public void Query(generalwork<dbContext, T> work)
        {
            work.ToQuery();
        }
        public void Update(generalwork<dbContext, T> work)
        {
            work.UpdateList();
        }
        public void Close(generalwork<dbContext, T> work)
        {
            work.Close();
        }


    }

}
