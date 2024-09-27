using SF.BikeTheft.Application.Models.DTOs;

namespace SF.BikeTheft.Application.Interfaces;

public interface IBikeTheftService
{
    Task<List<BikeTheftDto>> GetBikeTheftsByDateRangeAsync(DateTime startDate, DateTime endDate);
}
