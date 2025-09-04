using System.ComponentModel.DataAnnotations;

namespace Server.Events;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;

    [StringLength(200)]
    public string Description { get; set; } = string.Empty;

    [StringLength(7)] // Hex-färg: #RRGGBB
    public string Color { get; set; } = "#3B82F6"; // Default blå

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public ICollection<Event> Events { get; set; } = new List<Event>();
}
