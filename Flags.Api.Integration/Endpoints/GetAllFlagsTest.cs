using Flags.Application.Dto;
using Flags.Tests.Common;
using FluentAssertions;
using Testcontainers.PostgreSql;

namespace Flags.Tests.Endpoints;

public sealed class GetAllFlagsTest(FlagsApplicationFactory factory, RequestsCommon requestsCommon)
    : IClassFixture<FlagsApplicationFactory>, IClassFixture<RequestsCommon>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
        .WithImage("postgres:15-alpine")
        .Build();

    [Fact]
    public async Task ShouldReturnAllFlags()
    {
        var client = factory.CreateClient();

        var response = await client.GetAsync("/flags");
        response.EnsureSuccessStatusCode();

        var content = await requestsCommon.GetRequestContent<IEnumerable<FlagDto>>(response);

        // This should do for now
        content.Should().HaveCount(2);
    }

    [Fact]
// This is not a really a good test, but it's good enough for now :)
    public async Task ShouldPatchNewValueToFlag()
    {
        var client = factory.CreateClient();

        // Check the state of the flags
        {
            var response = await client.GetAsync("/flags");
            response.EnsureSuccessStatusCode();

            var content = await requestsCommon.GetRequestContent<IEnumerable<FlagDto>>(response);

            content.Should().SatisfyRespectively(
                first => first.Value.Should().BeTrue(),
                second => second.Value.Should().BeFalse()
            );
        }

        // Check if after patching you get 
        {
            var patchData = new PatchFlagCmd(Value: false);
            var dataJson = RequestsCommon.BuildRequestContent(patchData);

            var response = await client.PatchAsync("/flags/1", dataJson);
            response.EnsureSuccessStatusCode();
            var content = await requestsCommon.GetRequestContent<FlagDto>(response);

            content!.Value.Should().Be(patchData.Value);
        }

        // Check if we actually changed the state
        {
            var response = await client.GetAsync("/flags");
            response.EnsureSuccessStatusCode();

            var content = await requestsCommon.GetRequestContent<IEnumerable<FlagDto>>(response);

            content.Should().SatisfyRespectively(
                first => first.Value.Should().BeFalse(),
                second => second.Value.Should().BeFalse()
            );
        }
    }

    public Task InitializeAsync()
    {
        return _postgres.StartAsync();
    }

    public Task DisposeAsync()
    {
        return _postgres.DisposeAsync().AsTask();
    }
}