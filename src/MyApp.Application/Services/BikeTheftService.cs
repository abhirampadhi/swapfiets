using AutoMapper;
using SF.BikeTheft.Application.Interfaces;
using SF.BikeTheft.Application.Models.DTOs;
using SF.BikeTheft.Infrastructure.ExternalServices;

namespace SF.BikeTheft.Application.Services;

public class BikeTheftService : IBikeTheftService
{
    private readonly BikeTheftApiService _bikeTheftApiService;
    private readonly IMapper _mapper;

    public BikeTheftService(BikeTheftApiService bikeTheftApiService, IMapper mapper)
    {
        _bikeTheftApiService = bikeTheftApiService;
        _mapper = mapper;
    }

    public async Task<List<BikeTheftDto>> GetBikeTheftsByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        var bikeThefts = await _bikeTheftApiService.GetBikeTheftsByDateRangeAsync(startDate, endDate);
        return _mapper.Map<List<BikeTheftDto>>(bikeThefts);
    }
}