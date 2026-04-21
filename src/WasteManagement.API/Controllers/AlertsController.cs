using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WasteManagement.API.Data;
using WasteManagement.API.Models;

namespace WasteManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlertsController : ControllerBase
{
    private readonly AppDbContext _db;
    public AlertsController(AppDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _db.Alerts.OrderByDescending(a => a.CreatedAt).ToListAsync();
        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Alert alert)
    {
        _db.Alerts.Add(alert);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { id = alert.Id }, alert);
    }
}
