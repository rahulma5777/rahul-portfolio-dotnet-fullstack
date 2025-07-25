using Microsoft.EntityFrameworkCore;
using NetFullStack.API.Models;

namespace NetFullStack.API.Data
{
    /// <summary>
    /// EF Core database context for the application.  Defines the
    /// DbSet properties and relationships between entities.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<TaskItem> TaskItems => Set<TaskItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one‑to‑many relationship between User and TaskItem
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.User)
                .WithMany(u => u.TaskItems)
                .HasForeignKey(t => t.UserId);
        }
    }
}