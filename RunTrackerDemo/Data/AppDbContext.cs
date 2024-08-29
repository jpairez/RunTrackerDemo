using Microsoft.EntityFrameworkCore;
using RunTrackerDemo.Models;

namespace RunTrackerDemo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<RunningActivity> RunningActivities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasMany(user => user.RunningActivity)
                        .WithOne(ra => ra.User)
                        .HasForeignKey(ra => ra.User.Id);
        }

    }
}
