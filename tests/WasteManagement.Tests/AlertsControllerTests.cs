using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WasteManagement.Tests;

public class AlertsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    public AlertsControllerTests(WebApplicationFactory<Program> factory) => _factory = factory;

    [Fact]
    public async Task GetAll_ReturnsHttpStatusCode200()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/alerts");
        response.EnsureSuccessStatusCode();
    }
}
