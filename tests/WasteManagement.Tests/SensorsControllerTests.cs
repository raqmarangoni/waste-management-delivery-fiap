using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WasteManagement.Tests;

public class SensorsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    public SensorsControllerTests(WebApplicationFactory<Program> factory) => _factory = factory;

    [Fact]
    public async Task Telemetry_ReturnsHttpStatusCode200()
    {
        var client = _factory.CreateClient();
        var content = new StringContent("{ \"ContainerId\": \"C1\", \"FillLevelPercent\": 50 }", Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/sensors/telemetry", content);
        response.EnsureSuccessStatusCode();
    }
}
