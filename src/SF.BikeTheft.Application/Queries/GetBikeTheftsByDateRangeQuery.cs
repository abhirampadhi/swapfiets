using MediatR;
using SF.BikeTheft.Application.Models.DTOs;

namespace SF.BikeTheft.Application.Queries;

public class GetBikeTheftsByDateRangeQuery : IRequest<List<BikeDto>>
{
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    public GetBikeTheftsByDateRangeQuery(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }
}
