using System.Data.Entity;

namespace ting.model.net
{
    public partial class sysContext : DbContext
    {
        //private static string cnn = withCnn.GetConnectionString("tingSystem");
        //public static string cnn { get; set; }
        public sysContext(string cnn)
        : base(cnn)
        //: base(@"Data Source = (localdb)\MSSQLLOCALDB; Initial Catalog = tingSystem; Integrated Security = True")
        //: base(@"Data Source = .; Initial Catalog = tingSystem; Integrated Security = True") 
        //: base(@"Data Source = 192.168.0.77; Initial Catalog = tingZXZY; User Id = sa; Password = h47A9Da48")
        {
            //Database.SetInitializer(new sysDBInitializer());

            if (! Database.Exists() )
            {
                Database.SetInitializer(new sysDBInitializer());
            }
        }


        public virtual DbSet<SetofBook> SetofBooks { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<LevelOne> LevelOnes { get; set; }
        public virtual DbSet<LevelTwo> LevelTwos { get; set; }
        public virtual DbSet<Operation> Operations { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Right> Rights { get; set; }
        public virtual DbSet<Accredit> Accredits { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
    }
}
