using MediatR;
using SF.BikeTheft.Application.Models.DTOs;

namespace SF.BikeTheft.Application.Queries;

public sealed class GetBikeTheftsCountQuery : IRequest<BikeCountDto>
{
    public GetBikeTheftsCountQuery(string city, int distance)
    {
        City = city;
        Distance = distance;
    }

    public string City { get; }
    public int Distance { get;  }
}

