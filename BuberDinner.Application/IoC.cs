using Microsoft.Extensions.DependencyInjection;

namespace BuberDinner.Application;

public static class IoC
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly((typeof(IoC).Assembly)));
        return services;
    }
}
