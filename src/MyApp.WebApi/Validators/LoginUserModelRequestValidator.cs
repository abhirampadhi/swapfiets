﻿using FluentValidation;
using SF.BikeTheft.Application.Models.Requests;

namespace SF.BikeTheft.WebApi.Validators;

public sealed class LoginUserModelRequestValidator : AbstractValidator<LoginUserModelRequest>
{
    public LoginUserModelRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("User name must be entered.")
            .MinimumLength(3).WithMessage("User name must be at least 3 characters long.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password must be entered.")
            .MinimumLength(4).WithMessage("Password must be at least 4 characters long.");
    }
}
