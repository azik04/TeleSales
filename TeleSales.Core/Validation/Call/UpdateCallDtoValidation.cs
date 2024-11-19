using FluentValidation;
using TeleSales.Core.Dto.Call;

namespace TeleSales.Core.Validation.Call;

public class UpdateCallDtoValidation : AbstractValidator<UpdateCallDto>
{
    public UpdateCallDtoValidation()
    {
        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Status düzgün seçilməlidir.");

        RuleFor(x => x.AcquisitionDate)
            .NotEmpty().WithMessage("Tapşırığın əldə olunma tarixi tələb olunur.");

        RuleFor(x => x.KanalId)
            .GreaterThan(0).WithMessage("Kanal seçilməlidir.");

        RuleFor(x => x.EntrepreneurName)
            .NotEmpty().WithMessage("Sahibkarın adı tələb olunur.")
            .Length(1, 100).WithMessage("Sahibkarın adı 1-dən 100 simvola qədər olmalıdır.");

        RuleFor(x => x.LegalName)
            .NotEmpty().WithMessage("Hüquqi adı tələb olunur.")
            .Length(1, 100).WithMessage("Hüquqi adı 1-dən 100 simvola qədər olmalıdır.");

        RuleFor(x => x.VOEN)
            .NotEmpty().WithMessage("VÖEN tələb olunur.")
            .Length(9).WithMessage("VÖEN düzgün olmalıdır (9 simvol).");

        RuleFor(x => x.PermissionStartDate)
            .NotEmpty().WithMessage("İcazənin başlanma tarixi tələb olunur.");

        RuleFor(x => x.PermissionNumber)
            .NotEmpty().WithMessage("İcazə nömrəsi tələb olunur.")
            .Length(1, 50).WithMessage("İcazə nömrəsi 1-dən 50 simvola qədər olmalıdır.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Ünvan tələb olunur.")
            .Length(1, 200).WithMessage("Ünvan 1-dən 200 simvola qədər olmalıdır.");

        RuleFor(x => x.ContactDetails)
            .NotEmpty().WithMessage("Əlaqə məlumatları tələb olunur.")
            .Length(1, 200).WithMessage("Əlaqə məlumatları 1-dən 200 simvola qədər olmalıdır.");

        RuleFor(x => x.Result)
            .IsInEnum().WithMessage("Nəticə düzgün seçilməlidir.");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("İstifadəçi seçilməlidir.");
    }
}

