using AutoMapper;
using MediatR;
using SF.BikeTheft.Application.Models.DTOs;
using SF.BikeTheft.Application.Queries;
using SF.BikeTheft.Infrastructure.Interface;

namespace SF.BikeTheft.Application.Handlers;

public class GetBikeTheftsByDateRangeHandler : IRequestHandler<GetBikeTheftsByDateRangeQuery, List<BikeTheftDto>>
{
    private readonly IBikeTheftService _bikeTheftService;
    private readonly IMapper _mapper;

    public GetBikeTheftsByDateRangeHandler(IBikeTheftService bikeTheftService, IMapper mapper)
    {
        _bikeTheftService = bikeTheftService;
        _mapper = mapper;
    }

    public async Task<List<BikeTheftDto>> Handle(GetBikeTheftsByDateRangeQuery request, CancellationToken cancellationToken)
    {
        var bikeThefts = await _bikeTheftService.GetBikeTheftsByDateRangeAsync(request.StartDate, request.EndDate);
        return _mapper.Map<List<BikeTheftDto>>(bikeThefts);
    }
}