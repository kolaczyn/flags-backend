using FeatureFlags.Application.Dto;
using FeatureFlags.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FeatureFlags.Api;

public static class MapGetAllFlagsEndpointExtensions
{
    public static void MapGetAllFlagsEndpoint(this WebApplication app)
    {
        app.MapGet("flags", async Task<IResult>
            ([FromServices] GetAllFlagsUseCase useCase, CancellationToken ct) =>
        {
            var result = await useCase.Execute(ct);
            return Results.Ok(result);
        }).Produces<IEnumerable<FlagDto>>();
    }
}