using Microsoft.EntityFrameworkCore;
using WasteManagement.API.Models;

namespace WasteManagement.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Collection> Collections => Set<Collection>();
    public DbSet<Alert> Alerts => Set<Alert>();
}
