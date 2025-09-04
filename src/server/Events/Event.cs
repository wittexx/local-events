using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Events;

public class Event
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    [Required]
    [StringLength(200)]
    public string Location { get; set; } = string.Empty;

    [StringLength(200)]
    public string Address { get; set; } = string.Empty;

    [StringLength(50)]
    public string City { get; set; } = string.Empty;

    [StringLength(10)]
    public string PostalCode { get; set; } = string.Empty;

    // Koordinater för karta
    [Column(TypeName = "decimal(10,8)")]
    public decimal? Latitude { get; set; }

    [Column(TypeName = "decimal(11,8)")]
    public decimal? Longitude { get; set; }

    // Evenemang-detaljer
    public int? MaxAttendees { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? Price { get; set; }

    public bool IsFree => Price == null || Price == 0;

    // Organisatör info
    [StringLength(100)]
    public string? OrganizerName { get; set; }

    [StringLength(100)]
    [EmailAddress]
    public string? OrganizerEmail { get; set; }

    [StringLength(20)]
    public string? OrganizerPhone { get; set; }

    // Metadata
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }

    // Beräknade egenskaper
    public bool IsUpcoming() => StartDate > DateTime.UtcNow;
    public bool IsOngoing() => DateTime.UtcNow >= StartDate && (EndDate == null || DateTime.UtcNow <= EndDate);
    public bool HasEnded() => EndDate != null && DateTime.UtcNow > EndDate;
    public TimeSpan? Duration => EndDate?.Subtract(StartDate);
}
