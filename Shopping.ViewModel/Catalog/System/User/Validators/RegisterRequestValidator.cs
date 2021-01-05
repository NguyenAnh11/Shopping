using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Shopping.ViewModel.Catalog.System.User.Validators
{
    public class RegisterRequestValidator:AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FristName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Frist name must be required")
                .Matches(@"^[a-zA-Z]+$")
                .WithMessage("Frist name must cotain letter")
                .MaximumLength(200)
                .WithMessage("Frist name must contain bellow 200 characters");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Last name must be required")
                .Matches(@"^[a-zA-Z]+$")
                .WithMessage("Frist name must cotain letter")
                .MaximumLength(200)
                .WithMessage("Last name must contain bellow 200 characters");
            
            RuleFor(x => x.DoB).NotNull().WithMessage("DoB must be required");

            RuleFor(x => x.Email)
                 .Cascade(CascadeMode.Stop)
                 .NotEmpty()
                 .WithMessage("Email must be required")
                 .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                 .WithMessage("Email must be valid email address");

            RuleFor(x => x.PhoneNumber)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Phone number must be required")
                .Matches(@"^[0-9]+$")
                .WithMessage("Phone nummber must contain only digit")
                .MaximumLength(10)
                .WithMessage("Phone number must contain 10 digit");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username must be required");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Password must be required")
                .MinimumLength(6)
                .WithMessage("Password contain at least 6 characters")
                .Matches(@"^[0-9]+$")
                .WithMessage("Password contain only digit");

            RuleFor(x => x.ConfirmPassword)
                   .Cascade(CascadeMode.Stop)
                   .NotEmpty()
                   .WithMessage("Confirm password must be required")
                   .Equal(x => x.Password)
                   .WithMessage("Confirm password not match")
                   .DependentRules(() =>
                   {
                       RuleFor(x => x.Password).NotEmpty();
                   });
        }
    }
}
