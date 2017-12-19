
using DTO.Api;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DAL.Api
{
    public class DataContexto: DbContext
    {
        public DataContexto(): base("name=Dbconn") {

            //this.Configuration.LazyLoadingEnabled = true;
            //this.Configuration.ProxyCreationEnabled = true;
        }

        public DbSet<Enderecos> Enderecos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
