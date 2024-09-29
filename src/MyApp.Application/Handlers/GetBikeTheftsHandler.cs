using AutoMapper;
using MediatR;
using SF.BikeTheft.Application.Models.DTOs;
using SF.BikeTheft.Application.Queries;
using SF.BikeTheft.Infrastructure.Interface;

namespace SF.BikeTheft.Application.Handlers;

public class GetBikeTheftsHandler : IRequestHandler<GetBikeTheftsQuery, List<BikeTheftDto>>
{
    private readonly IBikeTheftApiService _bikeTheftService;
    private readonly IMapper _mapper;

    public GetBikeTheftsHandler(IBikeTheftApiService bikeTheftService, IMapper mapper)
    {
        _bikeTheftService = bikeTheftService;
        _mapper = mapper;
    }

    public async Task<List<BikeTheftDto>> Handle(GetBikeTheftsQuery request, CancellationToken cancellationToken)
    {
        var thefts = await _bikeTheftService.GetBikeTheftsAsync(request.City, request.Distance);
        return _mapper.Map<List<BikeTheftDto>>(thefts);
    }
}
