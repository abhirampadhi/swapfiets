using Newtonsoft.Json;
using SF.BikeTheft.Domain.Entities;
using SF.BikeTheft.Infrastructure.Interface;

namespace SF.BikeTheft.Infrastructure.ExternalServices;

public class BikeTheftApiService : IBikeTheftApiService
{
    private readonly IHttpClientWrapper _httpClientWrapper;

    public BikeTheftApiService(IHttpClientWrapper httpClientWrapper)
    {
        _httpClientWrapper = httpClientWrapper;
    }

    public async Task<List<BikeEntity>> GetBikeTheftsAsync(string city, int distance)
    {
        try
        {
            var response = await _httpClientWrapper.GetAsync($"https://bikeindex.org:443/api/v3/search?page=1&per_page=25&location={city}&distance={distance}&stolenness=proximity");

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
            // Log or handle the error
            Console.WriteLine($"JSON Deserialization Error: {ex.Message}");
            return new List<BikeEntity>();
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

    public async Task<List<BikeEntity>> GetBikeTheftsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        var response = await _httpClientWrapper.GetAsync($"/api/v3/search?location={startDate}&distance={endDate}&stolenness=proximity");
        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<BikeEntity>>(data);
    }

    //public async Task<int> GetBikeTheftCountAsync(string city, int distance)
    //{
    //    var response = await _httpClientWrapper.GetAsync($"/api/v3/search/count?location={city}&distance={distance}&stolenness=proximity");
    //    response.EnsureSuccessStatusCode();

    //    var data = await response.Content.ReadAsStringAsync();
    //    return JsonConvert.DeserializeObject<int>(data);
    //}

    //public async Task<BikeTheftEntity> GetBikeTheftByIdAsync(int id)
    //{
    //    var response = await _httpClientWrapper.GetAsync($"/api/v3/bikes/{id}");
    //    response.EnsureSuccessStatusCode();

    //    var data = await response.Content.ReadAsStringAsync();
    //    return JsonConvert.DeserializeObject<BikeTheftEntity>(data);
    //}


}

