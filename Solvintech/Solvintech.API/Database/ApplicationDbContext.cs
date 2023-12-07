using Microsoft.EntityFrameworkCore;
using Solvintech.API.Models;

namespace Solvintech.API.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        { }
    }
}
