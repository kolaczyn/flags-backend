using FeatureFlags.Application.Dto;
using FeatureFlags.Application.UseCases;
using FeatureFlags.Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace FeatureFlags.Api;

public static class MapPatchFlagEndpointExtensions
{
    public static void MapPatchFlagEndpointExtension(this WebApplication app)
    {
        app.MapPatch("{id}", Task<IResult>
            ([FromRoute] string id, [FromBody] PatchFlagCmd cmd,
                [FromServices] PatchFlagUseCase useCase, CancellationToken ct) =>
            {
                var (result, err) = useCase.Execute(id, cmd, ct);

                if (err == null) return Task.FromResult(Results.Ok(result));

                return Task.FromResult(err switch
                {
                    FlagDoesNotExist => Results.NotFound(err),
                    _ => Results.InternalServerError()
                });
            })
            .Produces<FlagDto>()
            .Produces(StatusCodes.Status404NotFound);
    }
}