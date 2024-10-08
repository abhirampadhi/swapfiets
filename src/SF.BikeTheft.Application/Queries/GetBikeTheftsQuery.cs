using MediatR;
using SF.BikeTheft.Application.Models.DTOs;

namespace SF.BikeTheft.Application.Queries;

public class GetBikeTheftsQuery : IRequest<List<BikeDto>>
{
    public string? City { get; }
    public int Distance { get; }
    public double? Longitude { get; }
    public double? Latitude { get; }

    public GetBikeTheftsQuery(int distance, string? city = null, double? longitude = null, double? latitude = null)
    {
        City = city;
        Distance = distance;
        Longitude = longitude;
        Latitude = latitude;
    }
}
