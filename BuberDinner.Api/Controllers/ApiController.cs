using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BuberDinner.Api.Controllers;

[ApiController]
public abstract class ApiController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    protected IActionResult Problem(params Error[] erros)
    {
        if (erros.Count() is 0) return Problem();

        if(erros.All(error => error.Type == ErrorType.Validation))
        {
            var modelStadeDictionary = new ModelStateDictionary();

            foreach(var error in erros)
            {
                modelStadeDictionary.AddModelError(
                    error.Code, 
                    error.Description);
            }

            return ValidationProblem(modelStadeDictionary);
        }

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
