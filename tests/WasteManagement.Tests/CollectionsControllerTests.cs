using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WasteManagement.Tests;

public class CollectionsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    public CollectionsControllerTests(WebApplicationFactory<Program> factory) => _factory = factory;

    [Fact]
    public async Task Get_ReturnsHttpStatusCode200()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/collections");
        response.EnsureSuccessStatusCode();
    }
}
