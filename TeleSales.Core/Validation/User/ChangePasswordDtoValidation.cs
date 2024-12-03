using FluentValidation;
using TeleSales.Core.Dto.User;

namespace TeleSales.Core.Validation.User;

public class ChangePasswordDtoValidation : AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordDtoValidation()
    {
        RuleFor(x => x.OldPassword)
            .NotEmpty().WithMessage("Köhnə şifrə tələb olunur.")
            .MinimumLength(6).WithMessage("Köhnə şifrə ən azı 6 simvoldan ibarət olmalıdır.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("Yeni şifrə tələb olunur.")
            .MinimumLength(8).WithMessage("Yeni şifrə ən azı 8 simvoldan ibarət olmalıdır.");

        RuleFor(x => x.ConfirmNewPassword)
            .NotEmpty().WithMessage("Yeni şifrənin təsdiqi tələb olunur.")
            .Equal(x => x.NewPassword).WithMessage("Təsdiq edilmiş şifrə yeni şifrə ilə eyni olmalıdır.");
    }
}

