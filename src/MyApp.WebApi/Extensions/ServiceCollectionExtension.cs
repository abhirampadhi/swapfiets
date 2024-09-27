using SF.BikeTheft.WebApi.Filters;
using System.Reflection;

namespace SF.BikeTheft.WebApi.Extensions;
public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApiService(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerFactory, LoggerFactory>()
                        .AddSingleton(typeof(ILogger<>), typeof(Logger<>))
                        .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            options.AddPolicy("User", policy => policy.RequireRole("User"));
        });


        services.AddControllers(options =>
        {
            options.Filters.Add<ValidationExceptionFilterAttribute>();
            options.Filters.Add<CustomExceptionFilter>();
        });

        //services.AddFluentValidationAutoValidation();
        //services.AddFluentValidationClientsideAdapters();
        //services.AddProblemDetails();

        //services.AddValidatorsFromAssemblyContaining<LoginUserModelRequestValidator>();
        //services.AddValidatorsFromAssemblyContaining<RegisterUserModelRequestValidator>();

        return services;
    }
}
