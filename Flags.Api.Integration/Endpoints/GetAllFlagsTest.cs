using Flags.Application.Dto;
using Flags.Tests.Common;
using FluentAssertions;
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

        var content = await response.GetRequestContent<IEnumerable<FlagDto>>();

        // This should do for now
        content.Should().HaveCount(2);
    }
}