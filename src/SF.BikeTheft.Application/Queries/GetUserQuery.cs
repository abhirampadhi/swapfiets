using MediatR;
using SF.BikeTheft.Application.Models.DTOs;

namespace SF.BikeTheft.Application.Queries;

public sealed class GetUserQuery : IRequest<UserDto>
{
    public string UserName { get; set; }
    public GetUserQuery(string userName)
    {
        UserName = userName;
    }
}
