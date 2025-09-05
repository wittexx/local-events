using System.ComponentModel.DataAnnotations;
using Server.Data;

namespace Server.Events;


public class CreateCategoryRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
}
public static class CreateCategoryEndpoints
{
    public static void MapCreateCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/api/categories", async (CreateCategoryRequest request, AppDbContext db) =>
        {
            var newCategory = new Category
            {
                Name = request.Name
            };
            db.Categories.Add(newCategory);
            await db.SaveChangesAsync();
            return Results.Created($"/api/categories/{newCategory.Id}", newCategory);
        })
        .WithName("CreateCategory")
        .WithSummary("Create a new Category")
        .WithTags("Categories")
        .Produces<Category>(201);
    }
}