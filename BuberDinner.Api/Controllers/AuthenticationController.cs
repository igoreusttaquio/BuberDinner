//using BuberDinner.Api.Filters;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[AllowAnonymous] // override ApiController
[ApiController]
[Route("api/auth")]
//[ErrorHadlingFilter]
public class AuthenticationController(ISender mediator, IMapper mapper) : ApiController
{
    private readonly ISender _mediator = mediator;
    private readonly IMapper _mapper = mapper;
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        //var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
        var command = _mapper.Map<RegisterCommand>(request);
        var resgisterResult = await _mediator.Send(command); //_authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);

        //var response = MapAuthResult(registerResult);
        //return Ok(response);
        //return Problem(result.AsT1.Message);
        var firstError = resgisterResult.Errors.FirstOrDefault();


        return resgisterResult.MatchFirst(
            // scoped
            resgisterResult => Ok(_mapper.Map<AuthenticationResponse>(resgisterResult)),
            error => Problem(error)
            );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        //var query = new LoginQuery(request.Email, request.Password);
        var query = _mapper.Map<LoginQuery>(request);
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
