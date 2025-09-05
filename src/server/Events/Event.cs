using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Events;

public class Event
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [Required]
    public DateTime DateTime { get; set; } 
    [Required]
    public string Location { get; set; } = string.Empty;


    public ICollection<Category> Categories { get; set; } = new List<Category>();
 
}

