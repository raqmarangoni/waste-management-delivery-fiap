using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WasteManagement.Tests;

public class ReportsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    public ReportsControllerTests(WebApplicationFactory<Program> factory) => _factory = factory;

    [Fact]
    public async Task Summary_ReturnsHttpStatusCode200()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/reports/summary");
        response.EnsureSuccessStatusCode();
    }
}
