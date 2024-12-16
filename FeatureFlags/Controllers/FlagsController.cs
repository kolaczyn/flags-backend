using Microsoft.AspNetCore.Mvc;

namespace FeatureFlags.Controllers;

[ApiController]
[Route("[controller]")]
public class FlagsController : ControllerBase
{
    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetFlags() => Ok(getFlags());


    private FlagsDto[] getFlags() =>
    [
        new()
        {
            Id = Guid.NewGuid()
                .ToString(),
            Value = true,
            Label = "greetUser"
        },
        new()
        {
            Id = Guid.NewGuid()
                .ToString(),
            Value = false,
            Label = "aboutSection"
        }
    ];
}