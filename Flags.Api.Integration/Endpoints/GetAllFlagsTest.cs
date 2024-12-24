using Flags.Application.Dto;
using Flags.Infrastructure.EFCore;
using Flags.Tests.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace Flags.Tests.Endpoints;

// https://github.com/testcontainers/testcontainers-dotnet/issues/1148#issuecomment-2035608104
// Thanks gbd3-en c)
public sealed class GetAllFlagsTest(RequestsCommon requestsCommon)
    : IClassFixture<RequestsCommon>, IAsyncLifetime
{
    private PostgreSqlContainer _postgres { get; set; } = null!;
    private WebApplicationFactory<Program> _factory { get; set; } = null!;
    private FlagsContext Context { get; set; } = null!;

    [Fact]
    public async Task ShouldReturnEmptyArrayIfNoFlags()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/flags");
        response.EnsureSuccessStatusCode();

        var content = await requestsCommon.GetRequestContent<IEnumerable<FlagDto>>(response);

        content.Should().HaveCount(0);
    }

    [Fact]
// This is not a really a good test, but it's good enough for now :)
    public async Task ShouldPatchNewValueToFlag()
    {
        var client = _factory.CreateClient();

        {
            var response = await client.GetAsync("/flags");
            response.EnsureSuccessStatusCode();

            var content = await requestsCommon.GetRequestContent<IEnumerable<FlagDto>>(response);

            content.Should().HaveCount(0);
        }
        {
            var payload = new PostFlagCmd(Label: "firstFlag");
            var dataJson = RequestsCommon.BuildRequestContent(payload);

            var response = await client.PostAsync("/flags", dataJson);
            response.EnsureSuccessStatusCode();
            var content = await requestsCommon.GetRequestContent<FlagDto>(response);

            content.Should().BeEquivalentTo(new FlagDto(Id: "1", Label: "firstFlag", Value: false));
        }
        {
            var payload = new PatchFlagCmd(Value: false);
            var dataJson = RequestsCommon.BuildRequestContent(payload);

            var response = await client.PatchAsync("/flags/1", dataJson);
            response.EnsureSuccessStatusCode();
            var content = await requestsCommon.GetRequestContent<FlagDto>(response);

            content.Should().BeEquivalentTo(new FlagDto(Id: "1", Label: "firstFlag", Value: false));
        }

        // TODO check GET
        {
        }
    }

    public async Task InitializeAsync()
    {
        _postgres = new PostgreSqlBuilder()
            .WithImage("postgres:15-alpine")
            .Build();

        await _postgres.StartAsync();
        await _postgres.WaitForPort();

        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddDbContextPool<FlagsContext>(options =>
                        options.UseNpgsql(_postgres.GetConnectionString(),
                            optionsBuilder => optionsBuilder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(5), null))
                    );
                });
            });

        await using var scope = _factory.Services.CreateAsyncScope();
        Context = scope.ServiceProvider.GetRequiredService<FlagsContext>();

        await Context.Database.MigrateAsync();
    }

    public async Task DisposeAsync()
    {
        await Context.DisposeAsync();
        await _postgres.DisposeAsync().AsTask();
    }
}