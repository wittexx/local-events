using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Server.Data;
using Scalar.AspNetCore;

// Create the web application builder
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Add CORS for frontend communication
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add API services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Local Events API", Version = "v1" });
});

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // Enable Swagger/OpenAPI
    app.UseSwagger();
    
    // Add Scalar API documentation
    app.MapScalarApiReference(options =>
    {
        options.Title = "Local Events API";
        options.Theme = ScalarTheme.BluePlanet;
        options.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseCors();
app.UseHttpsRedirection();

// API Routes
app.MapGet("/", () => "Local Events API is running!")
   .WithName("GetRoot")
   .WithSummary("API Status")
   .WithDescription("Returns a simple status message to confirm the API is running")
   .WithTags("Status");

app.MapGet("/api/health", () => new { 
       status = "healthy", 
       timestamp = DateTime.UtcNow,
       version = "1.0.0",
       environment = app.Environment.EnvironmentName
   })
   .WithName("GetHealthCheck")
   .WithSummary("Health Check")
   .WithDescription("Returns the current health status of the API")
   .WithTags("Health")
   .Produces<object>(200);

// Sample Events endpoint for testing
app.MapGet("/api/events", () => new[] {
       new {
           Id = 1,
           Title = "Konstutställning i Gamla Stan",
           Description = "En fantastisk utställning med lokala konstnärer",
           StartDate = DateTime.Today.AddDays(7),
           Location = "Galleri Stockholm",
           City = "Stockholm",
           IsFree = true
       },
       new {
           Id = 2,
           Title = "Yoga i parken",
           Description = "Gratis yoga för alla nivåer",
           StartDate = DateTime.Today.AddDays(3),
           Location = "Tantolunden",
           City = "Stockholm", 
           IsFree = true
       }
   })
   .WithName("GetEvents")
   .WithSummary("Get all events")
   .WithDescription("Returns a list of all local events")
   .WithTags("Events")
   .Produces<object[]>(200);

app.MapGet("/api/events/{id:int}", (int id) => {
       if (id == 1) {
           return Results.Ok(new {
               Id = 1,
               Title = "Konstutställning i Gamla Stan",
               Description = "En fantastisk utställning med lokala konstnärer",
               StartDate = DateTime.Today.AddDays(7),
               EndDate = DateTime.Today.AddDays(7).AddHours(6),
               Location = "Galleri Stockholm",
               Address = "Gamla Stan 15",
               City = "Stockholm",
               PostalCode = "11129",
               Price = (decimal?)null,
               IsFree = true,
               OrganizerName = "Stockholms Konstförening",
               OrganizerEmail = "info@konstforening.se"
           });
       }
       return Results.NotFound(new { message = $"Event with id {id} not found" });
   })
   .WithName("GetEventById")
   .WithSummary("Get event by ID")
   .WithDescription("Returns a specific event by its ID")
   .WithTags("Events")
   .Produces<object>(200)
   .Produces<object>(404);

// TODO: Add your real CRUD endpoints here
// app.MapEventsEndpoints();
// app.MapCategoriesEndpoints();

// Start the server
app.Run();
