using AutoMapper;
using MediatR;
using SF.BikeTheft.Application.Models.DTOs;
using SF.BikeTheft.Application.Queries;
using SF.BikeTheft.Infrastructure.Interface;
namespace SF.BikeTheft.Application.Handlers;

public sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByUserNameAsync(request.UserName);
        return _mapper.Map<UserDto>(user);
    }
}