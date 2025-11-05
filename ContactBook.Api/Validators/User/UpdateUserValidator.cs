using FluentValidation;
using ContactBook.Application.DTOs.User;

namespace ContactBook.Api.Validators.User;
public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("User Id must be greater than 0.");

        RuleFor(x => x.Name)
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
            .When(x => !string.IsNullOrEmpty(x.Name));

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email is not valid.")
            .When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.Now).WithMessage("Date of birth cannot be in the future.")
            .When(x => x.DateOfBirth.HasValue);
    }
}
