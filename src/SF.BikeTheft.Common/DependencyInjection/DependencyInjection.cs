using Microsoft.Extensions.DependencyInjection;
using SF.BikeTheft.Common.JwtWrapper;

namespace SF.BikeTheft.Common.DependencyInjection;

public static class DependencyInjections
{
    public static void AddCommonConfigureServices(this IServiceCollection services)
    {
        services.AddSingleton<IJwtTokenHandler, JwtTokenHandlerWrapper>();
    }
}
