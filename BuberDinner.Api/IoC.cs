using BuberDinner.Api.Common.Mapping;
using Mapster;

namespace BuberDinner.Api
{
    public static class IoC
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddMapster();
            services.AddMappings();
            return services;
        }
    }
}
