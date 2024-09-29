using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using SF.BikeTheft.Application.DependencyInjections;
using SF.BikeTheft.Infrastructure.DependencyInjection;
using SF.BikeTheft.WebApi.Extensions;
using SF.BikeTheft.Common.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

//The Log folder is temporary arrangemnet, recommendation is to use Azure Blob 
// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(new CompactJsonFormatter(), "Logs/BikeTheft-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();


var appSettings = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .Build();

// Add JWT authentication
builder.Services.AddJwtAuthentication(builder.Configuration);
// Automapper service
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationService();
builder.Services.AddInfrastructureService();
builder.Services.AddCommonConfigureServices();
builder.Services.AddApiService();
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation();
    app.UseDeveloperExceptionPage();
}

// Enable authentication and authorization
app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
