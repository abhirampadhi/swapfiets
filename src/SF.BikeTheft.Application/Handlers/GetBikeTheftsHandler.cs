using AutoMapper;
using MediatR;
using SF.BikeTheft.Application.Models.DTOs;
using SF.BikeTheft.Application.Queries;
using SF.BikeTheft.Infrastructure.Interface;

namespace SF.BikeTheft.Application.Handlers;

public class GetBikeTheftsHandler : IRequestHandler<GetBikeTheftsQuery, List<BikeDto>>
{
    private readonly IBikeTheftApiService _bikeTheftService;
    private readonly IMapper _mapper;

    public GetBikeTheftsHandler(IBikeTheftApiService bikeTheftService, IMapper mapper)
    {
        _bikeTheftService = bikeTheftService;
        _mapper = mapper;
    }

    public async Task<List<BikeDto>> Handle(GetBikeTheftsQuery request, CancellationToken cancellationToken)
    {
        if (request == null)
            return new List<BikeDto>();

        var location = string.Empty;
        if (!string.IsNullOrEmpty(request.City))
        {
            location = request.City; 
        }
        else if (request.Latitude.HasValue && request.Longitude.HasValue)
        {
            location = $"{request.Latitude.Value},{request.Longitude.Value}"; 
        }

        var thefts = await _bikeTheftService.GetBikeTheftsAsync(location, request.Distance);

        return _mapper.Map<List<BikeDto>>(thefts);
    }

}
