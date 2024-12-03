using FluentValidation;
using TeleSales.Core.Dto.User;

namespace TeleSales.Core.Validation.User;

public class UpdateUserDtoValidation : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email tələb olunur.")
            .EmailAddress().WithMessage("Düzgün email daxil edin.")
            .Length(1, 50).WithMessage("Email 1-dən 50 simvola qədər olmalıdır.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ad tələb olunur.") // Исправлено сообщение
            .Length(1, 20).WithMessage("Ad 1-dən 20 simvola qədər olmalıdır."); // Исправлено сообщение

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyad tələb olunur.") // Исправлено сообщение
            .Length(1, 20).WithMessage("Soyad 1-dən 20 simvola qədər olmalıdır."); // Исправлено сообщение
    }
}
