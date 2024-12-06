
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{

    public class AppDbContext : DbContext
    {
        // Define a table for TimeCapsules
        public DbSet<TimeCapsule> TimeCapsules { get; set; }

        // Pass options to the base class
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }

    }

}