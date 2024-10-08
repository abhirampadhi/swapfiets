﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SF.BikeTheft.Infrastructure.Data;
using SF.BikeTheft.Infrastructure.ExternalServices;
using SF.BikeTheft.Infrastructure.HttpClients;
using SF.BikeTheft.Infrastructure.Interface;
using SF.BikeTheft.Infrastructure.Policies;
using SF.BikeTheft.Infrastructure.Repositories;

namespace SF.BikeTheft.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services)
    {
        services.AddHttpClient<IHttpClientWrapper, HttpClientWrapper>()
        .AddPolicyHandler((provider, message) =>
        {
            var logger = provider.GetRequiredService<ILogger<HttpClientWrapper>>();
            return PolicyRegistry.GetRetryPolicy(logger);
        })
        .AddPolicyHandler((provider, message) =>
        {
            var logger = provider.GetRequiredService<ILogger<HttpClientWrapper>>();
            return PolicyRegistry.GetCircuitBreakerPolicy(logger);
        });


        // Register SwapfietsDbContext with in-memory database for assignment
        services.AddDbContext<SwapfietsDbContext>(options =>
            options.UseInMemoryDatabase("SwapfietsDbContext"));

        // Register UserDbContext with in-memory database for assignment
        services.AddDbContext<UserDbContext>(options =>
            options.UseInMemoryDatabase("UserDB"));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBikeTheftApiService, BikeTheftApiService>();

        return services;
    }

}
