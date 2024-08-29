using Microsoft.EntityFrameworkCore;

namespace MauiApiServer.Data.Core.Models
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(Configuration.GetConnectionString("DbConnection"));
        }

        public DbSet<Person> Persons { get; set; }
    }
}
