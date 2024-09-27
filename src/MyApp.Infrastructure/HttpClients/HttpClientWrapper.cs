using Microsoft.Extensions.Logging;
using Polly;
using SF.BikeTheft.Infrastructure.Interface;
using SF.BikeTheft.Infrastructure.Policies;

namespace SF.BikeTheft.Infrastructure.HttpClients;

public class HttpClientWrapper : IHttpClientWrapper
{
    private readonly HttpClient _httpClient;
    private readonly IAsyncPolicy<HttpResponseMessage> _retryPolicy;
    private readonly IAsyncPolicy<HttpResponseMessage> _circuitBreakerPolicy;
    private readonly ILogger<HttpClientWrapper> _logger;

    public HttpClientWrapper(HttpClient httpClient, ILogger<HttpClientWrapper> logger)
    {
        _httpClient = httpClient;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _retryPolicy = PolicyRegistry.GetRetryPolicy(_logger);
        _circuitBreakerPolicy = PolicyRegistry.GetCircuitBreakerPolicy(_logger);
    }

    public async Task<HttpResponseMessage> GetAsync(string requestUri)
    {
        return await ExecuteAsync(() => _httpClient.GetAsync(requestUri));
    }

    public async Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
    {
        return await ExecuteAsync(() => _httpClient.PostAsync(requestUri, content));
    }

    public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
    {
        return await ExecuteAsync(() => _httpClient.SendAsync(request));
    }

    private async Task<HttpResponseMessage> ExecuteAsync(Func<Task<HttpResponseMessage>> action)
    {
        try
        {
            return await _retryPolicy.ExecuteAsync(() =>
                _circuitBreakerPolicy.ExecuteAsync(action)
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during HTTP request execution.");
            throw;
        }
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}