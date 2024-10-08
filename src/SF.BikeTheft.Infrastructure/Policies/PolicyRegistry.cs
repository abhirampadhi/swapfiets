using Microsoft.Extensions.Logging;
using Polly;

namespace SF.BikeTheft.Infrastructure.Policies;

public static class PolicyRegistry
{
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(ILogger logger)
    {
        return Policy
            .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .Or<HttpRequestException>()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (outcome, timespan, retryAttempt, context) =>
                {
                    logger.LogWarning($"Retry {retryAttempt} encountered. Waiting {timespan} before next retry. Reason: {outcome.Exception?.Message ?? outcome.Result.ReasonPhrase}");
                });
    }

    public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(ILogger logger)
    {
        return Policy
            .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .Or<HttpRequestException>()
            .CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: 5,
                durationOfBreak: TimeSpan.FromSeconds(10),
                onBreak: (outcome, timespan) =>
                {
                    logger.LogError($"Circuit broken! Waiting {timespan} before next attempt. Reason: {outcome.Exception?.Message ?? outcome.Result.ReasonPhrase}");
                },
                onReset: () =>
                {
                    logger.LogInformation("Circuit reset.");
                });
    }
}