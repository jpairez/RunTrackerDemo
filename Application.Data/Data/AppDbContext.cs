using Application.RunTracker.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.RunTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public AppDbContext() : base() { }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<RunningActivity> RunningActivities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasMany(user => user.RunningActivities)
                        .WithOne(ra => ra.User)
                        .HasForeignKey(ra => ra.UserId);
        }
    }
}
