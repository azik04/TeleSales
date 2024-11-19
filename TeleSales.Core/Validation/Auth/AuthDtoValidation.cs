using FluentValidation;
using TeleSales.Core.Dto.AUTH;

namespace TeleSales.Core.Validation.Auth;

public class AuthDtoValidation : AbstractValidator<AuthDto>
{
    public AuthDtoValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email tələb olunur.")
            .EmailAddress().WithMessage("Düzgün email daxil edin.")
            .Length(1, 50).WithMessage("Email 1-dən 50 simvola qədər olmalıdır.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifrə tələb olunur.")
            .Length(1, 20).WithMessage("Şifrə 1-dən 20 simvola qədər olmalıdır.");
    }
}