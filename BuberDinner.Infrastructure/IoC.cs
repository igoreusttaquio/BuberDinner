using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Services;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Infrastructure.Persistence;

namespace BuberDinner.Infrastructure;

public static class Ioc
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));// need to use IOPtions
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
}
