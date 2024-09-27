using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using SF.BikeTheft.Application.DependencyInjections;
using SF.BikeTheft.Infrastructure.DependencyInjection;
using SF.BikeTheft.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(new CompactJsonFormatter(), "Logs/ProductInsuranceLog-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var appSettings = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .Build();

// Automapper service
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddApiService();
builder.Services.AddApplicationService();
builder.Services.AddInfrastructureService();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
