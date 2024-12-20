using Flags.Application.Dto;
using Flags.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Flags.Api.Endpoints;

public static class MapPatchFlagEndpointExtensions
{
    public static void MapPatchFlagEndpointExtension(this WebApplication app)
    {
        app.MapPatch("flags/{id}", Task<IResult>
            ([FromRoute] string id, [FromBody] PatchFlagCmd cmd,
                [FromServices] PatchFlagUseCase useCase, CancellationToken ct) =>
            {
                var result = useCase.Execute(id, cmd, ct);
                return result switch
                {
                    { IsSuccess: true } => Task.FromResult(Results.Ok(result.Value)),
                    // TODO I should throw 500 in case something unexpected happens
                    _ => Task.FromResult(Results.NotFound())
                };
            })
            .Produces<FlagDto>()
            .Produces(StatusCodes.Status404NotFound);
    }
}