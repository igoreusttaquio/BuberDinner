using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[ApiController]
public abstract class ApiController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult Problem(params Error[] erros)
    {
        var firstError = erros[0];

        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,

        };

        return Problem(statusCode: statusCode, title: firstError.Description);
    }
}
