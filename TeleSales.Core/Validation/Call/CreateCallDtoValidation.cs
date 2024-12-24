using FluentValidation;
using TeleSales.Core.Dto.Call;

namespace TeleSales.Core.Validation.Call;

public class CreateCallDtoValidation : AbstractValidator<CreateCallDto>
{
    public CreateCallDtoValidation()
    {

        RuleFor(x => x.KanalId)
            .GreaterThan(0).WithMessage("Kanal seçilməlidir.");

        RuleFor(x => x.LegalName)
            .NotEmpty().WithMessage("Hüquqi adı tələb olunur.")
            .Length(1, 100).WithMessage("Hüquqi adı 1-dən 100 simvola qədər olmalıdır.");

        RuleFor(x => x.VOEN)
            .NotEmpty().WithMessage("VÖEN tələb olunur.");

        RuleFor(x => x.PermissionStartDate)
            .NotEmpty().WithMessage("İcazənin başlanma tarixi tələb olunur.");

        

        RuleFor(x => x.Phone)
           .NotEmpty().WithMessage("Phone tələb olunur.")
           .Length(1, 200).WithMessage("Ünvan 1-dən 200 simvola qədər olmalıdır.");

    }
}
