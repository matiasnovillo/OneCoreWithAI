using Microsoft.EntityFrameworkCore;
using OneCore.Entities.EntitiesConfiguration;

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
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new BillConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentConfiguration());
            modelBuilder.ApplyConfiguration(new CustomLoggerConfiguration());

            modelBuilder.Entity<User>().HasData(new User { 
                UserId = 1,
                Email = "novillo.matias1@gmail.com",
                Password = "Pq5FM4q7dDtlZBGcn0w8P0XjnEPDlTCcLUY5/bWVcuVJ4/kXRyHp62hPgry0R/ur+kEspHc+HK6XqqvA8OLXLw=="
            });
        }
    }
}
