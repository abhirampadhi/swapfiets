using MediatR;
using SF.BikeTheft.Application.Models.DTOs;

namespace SF.BikeTheft.Application.Queries;

public class GetBikeTheftsQuery : IRequest<List<BikeDto>>
{
    public string City { get; set; }
    public int Distance { get; set; }

    public GetBikeTheftsQuery(string city, int distance)
    {
        City = city;
        Distance = distance;
    }
}

