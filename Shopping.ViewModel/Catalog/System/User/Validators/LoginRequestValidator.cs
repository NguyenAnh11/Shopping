using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopping.ViewModel.Catalog.System.User.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Email must be required")
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                .WithMessage("Email must be valid email address");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Password must be required")
                .MinimumLength(6)
                .WithMessage("Password contain at least 6 characters")
                .Matches(@"^[0-9]+$")
                .WithMessage("Password contain only digit");

            RuleFor(x => x.RememberMe).NotNull()
                                    .WithMessage("Rememberme must chooose");
        }
    }
}
