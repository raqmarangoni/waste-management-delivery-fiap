using Microsoft.AspNetCore.Mvc;

namespace WasteManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SensorsController : ControllerBase
{
    // Simulated sensor endpoint for IoT integration
    [HttpPost("telemetry")]
    public IActionResult Telemetry([FromBody] SensorDto dto)
    {
        if (dto == null) return BadRequest();
        if (dto.FillLevelPercent > 90) return StatusCode(202, new { alert = "Container almost full" });
        return Ok(new { received = true });
    }
}

public record SensorDto(string ContainerId, double FillLevelPercent);
