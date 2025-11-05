using FluentValidation;
using ContactBook.Shared.DTOs.Phone;

namespace ContactBook.Api.Validators.Phone;

public class UpdatePhoneDtoValidator : AbstractValidator<UpdatePhoneDto>
{
    public UpdatePhoneDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Phone Id must be greater than 0.");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?\d{10,15}$").WithMessage("Phone number is invalid.")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber));

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId must be greater than 0.")
            .When(x => x.UserId.HasValue);
    }
}
