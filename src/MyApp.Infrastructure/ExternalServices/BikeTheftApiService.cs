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

    public async Task<List<BikeTheftEntity>> GetBikeTheftsAsync(string city, int distance)
    {
        var response = await _httpClientWrapper.GetAsync($"/api/v3/search?location={city}&distance={distance}&stolenness=proximity");
        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<BikeTheftEntity>>(data);
    }

    public async Task<List<BikeTheftEntity>> GetBikeTheftsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        var response = await _httpClientWrapper.GetAsync($"/api/v3/search?location={startDate}&distance={endDate}&stolenness=proximity");
        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<BikeTheftEntity>>(data);
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

