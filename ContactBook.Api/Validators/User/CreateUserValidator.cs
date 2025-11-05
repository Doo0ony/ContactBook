using FluentValidation;
using ContactBook.Application.DTOs.User;

namespace ContactBook.Api.Validators.User;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is not valid.")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .LessThan(DateTime.Now).WithMessage("Date of birth cannot be in the future.");
    }
}
