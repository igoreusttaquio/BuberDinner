//using BuberDinner.Api.Filters;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[ApiController]
[Route("api/auth")]
//[ErrorHadlingFilter]
public class AuthenticationController(IAuthenticationService authenticationService) : ApiController
{
    private readonly IAuthenticationService _authenticationService = authenticationService;
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var resgisterResult = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);

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
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationService.Login(request.Email, request.Password);

        return authResult.Match(
            sucess => Ok(MapAuthResult(sucess)),
            errors => Problem([.. errors]));
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(authResult.User.Id, authResult.User.FirstName, authResult.User.LastName, authResult.User.Email, authResult.Token);
    }
}
