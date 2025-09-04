using Microsoft.EntityFrameworkCore;
using Server.Events;

namespace Server.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Event configuration
        modelBuilder.Entity<Event>(entity =>
        {
         
        });

        // Category configuration
        modelBuilder.Entity<Category>(entity =>
        {

        });

        // Seed some default categories
        modelBuilder.Entity<Category>().HasData(

        );
    }
}
