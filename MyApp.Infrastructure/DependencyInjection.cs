using Microsoft.Extensions.DependencyInjection;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Services;  // This should match your namespace

namespace MyApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            return services;
        }
    }
}
