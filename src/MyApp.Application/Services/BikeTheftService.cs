using SF.BikeTheft.Application.Interfaces;
using SF.BikeTheft.Application.Models.DTOs;

namespace SF.BikeTheft.Application.Services;

public class BikeTheftService : IBikeTheftService
{
    private readonly IBikeTheftRepository _repository;

    public BikeTheftService(IBikeTheftRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<BikeTheftDto>> GetBikeTheftsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await _repository.GetBikeTheftsByDateRangeAsync(startDate, endDate);
    }
}