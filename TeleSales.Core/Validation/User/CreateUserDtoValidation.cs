using FluentValidation;
using TeleSales.Core.Dto.User;

namespace TeleSales.Core.Validation.User;

public class CreateUserDtoValidation : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email tələb olunur.")
            .EmailAddress().WithMessage("Düzgün email daxil edin.")
            .Length(1, 50).WithMessage("Email 1-dən 50 simvola qədər olmalıdır.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ad tələb olunur.") 
            .Length(1, 20).WithMessage("Ad 1-dən 20 simvola qədər olmalıdır.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyad tələb olunur.") 
            .Length(1, 20).WithMessage("Soyad 1-dən 20 simvola qədər olmalıdır.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifrə tələb olunur.")
            .MinimumLength(8).WithMessage("Yeni şifrə ən azı 8 simvoldan ibarət olmalıdır.");
    }
}
