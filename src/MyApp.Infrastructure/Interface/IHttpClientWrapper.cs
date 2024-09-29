namespace SF.BikeTheft.Infrastructure.Interface;

public interface IHttpClientWrapper
{
    Task<HttpResponseMessage> GetAsync(string requestUri);
    Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);
    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
}
