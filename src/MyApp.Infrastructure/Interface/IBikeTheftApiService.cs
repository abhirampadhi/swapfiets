using SF.BikeTheft.Domain.Entities;
namespace SF.BikeTheft.Infrastructure.Interface;

public interface IBikeTheftApiService
{
    Task<List<BikeTheftEntity>> GetBikeTheftsAsync(string city, int distance);
    Task<List<BikeTheftEntity>> GetBikeTheftsByDateRangeAsync(DateTime startDate, DateTime endDate);
}
