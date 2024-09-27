using SF.BikeTheft.Domain.Entities;

namespace SF.BikeTheft.Infrastructure.Interface;

public interface IBikeTheftService
{
    Task<List<BikeTheftEntity>> GetBikeTheftsAsync(string city, int distance);
}
