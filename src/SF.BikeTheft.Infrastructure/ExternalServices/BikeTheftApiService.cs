using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SF.BikeTheft.Domain.Entities;
using SF.BikeTheft.Infrastructure.Interface;

namespace SF.BikeTheft.Infrastructure.ExternalServices;

public class BikeTheftApiService : IBikeTheftApiService
{
    private readonly IHttpClientWrapper _httpClientWrapper;
    private readonly IConfiguration _configuration;

    public BikeTheftApiService(IHttpClientWrapper httpClientWrapper, IConfiguration configuration)
    {
        _httpClientWrapper = httpClientWrapper;
        _configuration = configuration;
    }

    public async Task<List<BikeEntity>> GetBikeTheftsAsync(string location, int distance)
    {
        try
        {
            var baseUrl = _configuration["BikeTheftApi:BaseUrl"];
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new Exception("BaseUrl configuration not found.");
            }

            var url = $"{baseUrl}?page=1&per_page=25&location={location}&distance={distance}&stolenness=proximity";
            var response = await _httpClientWrapper.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to retrieve data: {response.StatusCode}");
            }

            var data = await response.Content.ReadAsStringAsync();
            var bikeApiResponse = JsonConvert.DeserializeObject<BikeListEntity>(data);

            return bikeApiResponse?.Bikes ?? new List<BikeEntity>();
        }
        catch (JsonException ex)
        {
            throw new Exception("An error occurred while processing the response data.", ex);
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("An error occurred while fetching bike theft data.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred.", ex);
        }
    }

    public async Task<BikeCountEntity> GetBikeTheftCountAsync(string location, int distance)
    {
        try
        {
            var baseUrl = _configuration["BikeTheftApi:BaseUrl"];
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new Exception("BaseUrl configuration not found.");
            }

            var url = $"{baseUrl}/count?location={location}&distance={distance}&stolenness=proximity";
            var response = await _httpClientWrapper.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to retrieve data: {response.StatusCode}");
            }

            var data = await response.Content.ReadAsStringAsync();
            var bikeApiResponse = JsonConvert.DeserializeObject<BikeCountEntity>(data);

            return bikeApiResponse ?? throw new Exception("No data received from the API.");
        }
        catch (JsonException ex)
        {
            throw new Exception("An error occurred while processing the response data.", ex);
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("An error occurred while fetching bike theft data.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred.", ex);
        }
    }

}
