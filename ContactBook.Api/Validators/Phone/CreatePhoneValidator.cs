using FluentValidation;
using ContactBook.Shared.DTOs.Phone;

namespace ContactBook.Api.Validators.Phone;

public class CreatePhoneDtoValidator : AbstractValidator<CreatePhoneDto>
{
    public CreatePhoneDtoValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?\d{10,15}$").WithMessage("Phone number is invalid.");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId must be greater than 0.");
    }
}