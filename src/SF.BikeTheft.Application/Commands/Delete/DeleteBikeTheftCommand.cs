﻿using MediatR;

namespace SF.BikeTheft.Application.Commands.Delete;

public sealed class DeleteBikeTheftCommand : IRequest
{
    public DeleteBikeTheftCommand(int id)
    {
            
    }
}
