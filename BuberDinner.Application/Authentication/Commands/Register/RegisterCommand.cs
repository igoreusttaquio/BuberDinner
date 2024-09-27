using BuberDinner.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Authentication.Commands.Register;

// encapsulate data that we need to run the command
public record class RegisterCommand(string FirstName, string LastName,
    string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;
