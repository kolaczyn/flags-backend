using Flags.Application.Dto;
using Flags.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Flags.Api.Endpoints;

public static class MapPostFlagEndpointExtensions
{
    public static void MapPostFlagEndpointExtension(this WebApplication app)
    {
        app.MapPost("flags", async Task<IResult>
            ([FromBody] PostFlagCmd cmd,
                [FromServices] PostFlagUseCase useCase, CancellationToken ct) =>
            {
                var result = await useCase.Execute(cmd, ct);
                return result switch
                {
                    { IsSuccess: true } => Results.Ok(result.Value),
                    _ => Results.NotFound()
                    // TODO I should throw 500 in case something unexpected happens
                };
            })
            .Produces<FlagDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);
    }
}