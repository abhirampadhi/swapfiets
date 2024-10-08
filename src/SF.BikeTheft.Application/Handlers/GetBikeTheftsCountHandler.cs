using AutoMapper;
using MediatR;
using SF.BikeTheft.Application.Models.DTOs;
using SF.BikeTheft.Application.Queries;
using SF.BikeTheft.Infrastructure.Interface;

namespace SF.BikeTheft.Application.Handlers;

public sealed class GetBikeTheftsCountHandler : IRequestHandler<GetBikeTheftsCountQuery, BikeCountDto> 
{
    private readonly IBikeTheftApiService _bikeTheftService;
    private readonly IMapper _mapper;

    public GetBikeTheftsCountHandler(IBikeTheftApiService bikeTheftService, IMapper mapper)
    {
        _bikeTheftService = bikeTheftService;
        _mapper = mapper;
    }

    public async Task<BikeCountDto> Handle(GetBikeTheftsCountQuery request, CancellationToken cancellationToken)
    {
        if (request == null)
            return new BikeCountDto();

        var location = request.City;
       

        var theftCount = await _bikeTheftService.GetBikeTheftCountAsync(location, request.Distance);

        return _mapper.Map<BikeCountDto>(theftCount);
    }
}
