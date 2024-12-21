using Microsoft.AspNetCore.Mvc.Testing;

namespace Flags.Tests.Endpoints;

public sealed class GetAllFlagsTest(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task ShouldReturnAllFlags()
    {
        var client = factory.CreateClient();

        var response = await client.GetAsync("/flags");
        response.EnsureSuccessStatusCode();
    }
}