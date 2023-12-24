using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OneCore.Entities.EntitiesConfiguration;
using System.Runtime;

namespace OneCore.Entities.Configuration
{
    public class EFCoreContext : DbContext
    {
        protected IConfiguration _configuration { get; }
        public EFCoreContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<User> User { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<CustomLogger> CustomLogger { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("OneCore"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BillConfiguration());
            modelBuilder.ApplyConfiguration(new CustomLoggerConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<User>().HasData(new User { 
                UserId = 1,
                Email = "novillo.matias1@gmail.com",
                Password = "z"
            });
        }
    }
}
