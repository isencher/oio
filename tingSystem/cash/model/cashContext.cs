using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ting.lib;
using ting.pal;

namespace ting.cash.model
{
    public class cashContext : DbContext
    {
        private static string cnn = withCnn.GetConnectionString("tingSystem", Start.Set.DbName);
        //private static string cnn = "Data Source = 192.168.0.77; Initial Catalog = tingZXZY; User Id = sa; Password = h47A9Da48";
        public cashContext()
            : base(cnn)
        {
            //Database.SetInitializer(new cashDBInitializer());

            if (!Database.Exists())
            {
                Database.SetInitializer(new cashDBInitializer());
            }
        }

        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<AccountCategory> AccountCategories { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<InOutCategory> InOutCategaries { get; set; }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<StandingBook> StandingBooks { get; set; }

    }
}
