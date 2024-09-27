using Microsoft.Extensions.DependencyInjection;
using SF.BikeTheft.Application.Interfaces;
using SF.BikeTheft.Application.Services;
using System.Reflection;

namespace SF.BikeTheft.Application.DependencyInjections;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddMediatR(cf => cf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
