using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Server.Events;

public class Category
{
    [Key]
  public int Id{ get; set;}

    [Required]
    public string Name { get; set; } = String.Empty;

  public ICollection<Event> Events { get; } = new List<Event>();
}
