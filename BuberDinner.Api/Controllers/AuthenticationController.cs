//using BuberDinner.Api.Filters;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("api/auth")]
//[ErrorHadlingFilter]
public class AuthenticationController(IMediator mediator) : ApiController
{
    private readonly IMediator _mediator = mediator;
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
        var resgisterResult = await _mediator.Send(command); //_authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);

        //var response = MapAuthResult(registerResult);
        //return Ok(response);
        //return Problem(result.AsT1.Message);
        var firstError = resgisterResult.Errors.FirstOrDefault();


        return resgisterResult.MatchFirst(
            // scoped
            resgisterResult => Ok(MapAuthResult(resgisterResult)),
            error => Problem(error)
            );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);
        var authResult = await _mediator.Send(query);

        return authResult.Match(
            sucess => Ok(MapAuthResult(sucess)),
            errors => Problem([.. errors]));
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(authResult.User.Id, authResult.User.FirstName, authResult.User.LastName, authResult.User.Email, authResult.Token);
    }
}
