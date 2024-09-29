using SF.BikeTheft.Application.Models.DTOs;

namespace SF.BikeTheft.Application.Interfaces;

public interface IBikeTheftService
{
    Task<List<BikeDto>> GetBikeTheftsByDateRangeAsync(DateTime startDate, DateTime endDate);
}
