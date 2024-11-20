using BuberDinner.Domain.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Domain.UserAggregate;

namespace BuberDinner.Application.Authentication.Commands.Register;

public class RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            //throw new DuplicateEmailException("User with give e-mail already exists!");
            return Errors.User.DuplicateEmail;
        }

        User user = User.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            command.Password
            );

        _userRepository.Add(user);
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}
