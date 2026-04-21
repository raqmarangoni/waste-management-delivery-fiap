using Microsoft.AspNetCore.Mvc;
using WasteManagement.API.Services;
using WasteManagement.API.ViewModels;

namespace WasteManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CollectionsController : ControllerBase
{
    private readonly ICollectionService _service;
    public CollectionsController(ICollectionService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var (items, total) = await _service.GetPagedAsync(page, pageSize);
        var result = new { page, pageSize, total, items };
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CollectionViewModel vm)
    {
        var id = await _service.CreateAsync(vm);
        return CreatedAtAction(nameof(Get), new { id = id }, vm);
    }
}
