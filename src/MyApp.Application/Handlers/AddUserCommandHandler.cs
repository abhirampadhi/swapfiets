using AutoMapper;
using MediatR;
using SF.BikeTheft.Application.Commands.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF.BikeTheft.Application.Handlers;

public sealed class AddUserCommandHandler : IRequestHandler<AddUserCommand>
{
    private readonly IUserRepository _repository;

    public AddUserCommandHandler(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
    }

    public async Task Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            IsActive = true,
            Name = request.Name,
            Password = request.Password,
            Roles = request.Roles,
            UserName = request.UserName
        };

        await _repository.AddUserAsync(user);
    }
}
