using Microsoft.EntityFrameworkCore;
using WasteManagement.API.Data;
using WasteManagement.API.Repositories;
using WasteManagement.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration & DbContext (SQLite file-based, easiest to run)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=waste.db";
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

// Repositories & Services
builder.Services.AddScoped<ICollectionRepository, CollectionRepository>();
builder.Services.AddScoped<ICollectionService, CollectionService>();

var app = builder.Build();

// Apply migrations / ensure database (for simplicity we use EnsureCreated to avoid requiring 'dotnet ef' on the student's machine)
// We also include SQL script in /migrations to allow DB creation elsewhere.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Make Program class available for integration tests (WebApplicationFactory)
public partial class Program { }
