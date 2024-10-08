using AutoMapper;
using MediatR;
using SF.BikeTheft.Application.Models.DTOs;
using SF.BikeTheft.Application.Queries;
using SF.BikeTheft.Infrastructure.Interface;

namespace SF.BikeTheft.Application.Handlers;

public sealed class GetBikeTheftsByDateRangeHandler : IRequestHandler<GetBikeTheftsByDateRangeQuery, List<BikeDto>>
{
    private readonly IBikeTheftApiService _bikeTheftService;
    private readonly IMapper _mapper;

    public GetBikeTheftsByDateRangeHandler(IBikeTheftApiService bikeTheftService, IMapper mapper)
    {
        _bikeTheftService = bikeTheftService;
        _mapper = mapper;
    }

    public async Task<List<BikeDto>> Handle(GetBikeTheftsByDateRangeQuery request, CancellationToken cancellationToken)
    {
        var bikeThefts = await _bikeTheftService.GetBikeTheftsByDateRangeAsync(request.StartDate, request.EndDate);
        return _mapper.Map<List<BikeDto>>(bikeThefts);
    }
}