using Newtonsoft.Json;
using SF.BikeTheft.Domain.Entities;
using SF.BikeTheft.Infrastructure.Interface;

namespace SF.BikeTheft.Infrastructure.Services;

public class BikeTheftService : IBikeTheftService
{
    private readonly HttpClient _httpClient;

    public BikeTheftService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<BikeTheftEntity>> GetBikeTheftsAsync(string city, int distance)
    {
        var response = await _httpClient.GetAsync($"https://bikeindex.org:443/api/v3/search?location={city}&distance={distance}&stolenness=proximity");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var bikeThefts = JsonConvert.DeserializeObject<List<BikeTheftEntity>>(content);
        return bikeThefts;
    }
}
