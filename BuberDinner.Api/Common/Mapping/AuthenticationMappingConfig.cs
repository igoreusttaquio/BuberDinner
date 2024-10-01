using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;
using Mapster;

namespace BuberDinner.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();
        // auth result to alth response
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
//            .Map(dest => dest.Token, src => src.Token) if properties has the same na does not need mapping
            .Map(dest => dest, src => src.User);
    }
}
