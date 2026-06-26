using Microsoft.EntityFrameworkCore;

namespace cement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<cement.Models.User> Users { get; set; }
    }
}
