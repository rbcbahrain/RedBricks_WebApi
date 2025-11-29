using System.Data.Common;
using Microsoft.EntityFrameworkCore;



namespace RedBricksApi.Models
{
    public class DbContextClass : DbContext

    {
        protected readonly IConfiguration Configuration;
        public DbContextClass(IConfiguration configuration)
        {

            this.Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

        }
       
        public DbSet<Product> Product { get; set; }
        public DbSet<User> User { get; set; }
       
        public DbSet<ProductType> ProductTypes { get; set; }
       

    }

}
