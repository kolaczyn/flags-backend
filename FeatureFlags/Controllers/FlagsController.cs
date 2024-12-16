using FeatureFlags.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FeatureFlags.Controllers;

[ApiController]
[Route("[controller]")]
public class FlagsController : ControllerBase
{
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetFlags([FromServices] GetAllFlagsUseCase useCase)
    {
        var result = useCase.Execute();
        return Ok(result);
    }
}