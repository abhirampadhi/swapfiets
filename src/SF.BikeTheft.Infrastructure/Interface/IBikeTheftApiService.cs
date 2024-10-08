using SF.BikeTheft.Domain.Entities;
namespace SF.BikeTheft.Infrastructure.Interface;

public interface IBikeTheftApiService
{
    Task<List<BikeEntity>> GetBikeTheftsAsync(string location, int distance);
    Task<BikeCountEntity> GetBikeTheftCountAsync(string location, int distance);
}
