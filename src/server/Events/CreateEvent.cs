using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Events;

public class CreateEventRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [Required]
    public DateTime DateTime { get; set; }
    public string Location { get; set; } = string.Empty;
}

public static class CreateEventEndpoints
{
    public static void MapCreateEventEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/events", async (CreateEventRequest request, AppDbContext db) =>
        {
            var newEvent = new Event
            {
                Name = request.Name,
                Description = request.Description,
                DateTime = request.DateTime,
                Location = request.Location
            };

            db.Events.Add(newEvent);
            await db.SaveChangesAsync();

            return Results.Created($"/api/events/{newEvent.Id}", newEvent);
        })
        .WithName("CreateEvent")
        .WithSummary("Create a new event")
        .WithTags("Events")
        .Produces<Event>(201);
    }
}