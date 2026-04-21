using Microsoft.AspNetCore.Mvc;
using WasteManagement.API.Data;
using Microsoft.EntityFrameworkCore;

namespace WasteManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly AppDbContext _db;
    public ReportsController(AppDbContext db) => _db = db;

    [HttpGet("summary")]
    public async Task<IActionResult> Summary()
    {
        var totalCollections = await _db.Collections.CountAsync();
        var totalWeight = await _db.Collections.SumAsync(c => (double?)c.WeightKg) ?? 0;
        var byMaterial = await _db.Collections
            .GroupBy(c => c.MaterialType)
            .Select(g => new { material = g.Key, weight = g.Sum(x => x.WeightKg) })
            .ToListAsync();

        return Ok(new { totalCollections, totalWeight, byMaterial });
    }
}
