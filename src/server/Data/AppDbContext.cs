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
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Location).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.OrganizerName).HasMaxLength(100);
            entity.Property(e => e.OrganizerEmail).HasMaxLength(100);
            entity.Property(e => e.OrganizerPhone).HasMaxLength(20);
            entity.Property(e => e.Price).HasPrecision(10, 2);
            entity.Property(e => e.Latitude).HasPrecision(10, 8);
            entity.Property(e => e.Longitude).HasPrecision(11, 8);
            
            // Relationship with Category
            entity.HasOne(e => e.Category)
                  .WithMany(c => c.Events)
                  .HasForeignKey(e => e.CategoryId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // Category configuration
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(50);
            entity.Property(c => c.Description).HasMaxLength(200);
            entity.Property(c => c.Color).HasMaxLength(7); // #RRGGBB
        });

        // Seed some default categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Konst & Kultur", Description = "Utställningar, konserter, teater", Color = "#8B5CF6" },
            new Category { Id = 2, Name = "Sport & Träning", Description = "Idrottsevenemang, träningspass", Color = "#10B981" },
            new Category { Id = 3, Name = "Mat & Dryck", Description = "Restauranger, barer, matfestivaler", Color = "#F59E0B" },
            new Category { Id = 4, Name = "Utbildning", Description = "Workshops, seminarier, kurser", Color = "#3B82F6" },
            new Category { Id = 5, Name = "Familj", Description = "Barnaktiviteter, familjeevenemang", Color = "#EF4444" }
        );
    }
}
