using System.Net;
using FeatureFlags.Application.Dto;
using FeatureFlags.Application.UseCases;
using FeatureFlags.Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace FeatureFlags.Controllers;

[ApiController]
[Route("[controller]")]
public class FlagsController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFlags([FromServices] GetAllFlagsUseCase useCase, CancellationToken ct)
    {
        var result = await useCase.Execute(ct);
        return Ok(result);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult PatchFlag([FromRoute] string id, [FromBody] PatchFlagCommand command,
        [FromServices] PatchFlagUseCase useCase, CancellationToken ct)
    {
        var (result, err) = useCase.Execute(id, command, ct);

        if (err == null) return Ok(result);

        return err switch
        {
            FlagDoesNotExist => NotFound(err),
            _ => StatusCode((int)HttpStatusCode.InternalServerError)
        };
    }
}