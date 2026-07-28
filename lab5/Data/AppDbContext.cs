using lab5.Models;
using Microsoft.EntityFrameworkCore;

namespace lab5.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<CV>Cvs { get; set; }
    }
}