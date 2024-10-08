using SF.BikeTheft.Domain.Entities;
namespace SF.BikeTheft.Infrastructure.Interface;

public interface IBikeTheftApiService
{
    Task<List<BikeEntity>> GetBikeTheftsAsync(string city, int distance);
    Task<List<BikeEntity>> GetBikeTheftsByDateRangeAsync(DateTime startDate, DateTime endDate);
}
