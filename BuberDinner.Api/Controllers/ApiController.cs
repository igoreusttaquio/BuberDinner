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

        //HttpContext.Items["errors"] = erros;
        // var details = string.Join(",", erros.Select(e => $"{e.Code} - {e.Description}"));

        //var details = new ProblemDetails
        //{
        //    Extensions = (IDictionary<string, object?>)erros.ToDictionary(erro => erro.Code, erro => erro),
        //    Status = statusCode,
        //    Title = firstError.Description
        //};


        return Problem(statusCode: statusCode, title: firstError.Description /*,detail: details*/);
    }
}
