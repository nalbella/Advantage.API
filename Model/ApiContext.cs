using Microsoft.EntityFrameworkCore;

namespace Advantage.API.Model
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Driver> Drivers { get; set; }
    }
}

