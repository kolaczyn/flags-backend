using Flags.Application.Dto;
using Flags.Application.UseCases;
using Flags.Domain.Errors;
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