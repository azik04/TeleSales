using FluentValidation;
using TeleSales.Core.Dto.Call;
using TeleSales.DataProvider.Enums.Debitor;

namespace TeleSales.Core.Validation.Call;

public class ExcludeCallDtoValidation : AbstractValidator<ExcludeCallDto>
{
    public ExcludeCallDtoValidation()
    {
        RuleFor(x => x.Conclusion)
                       .IsInEnum().WithMessage("Conclusion must be a valid CallResult.");

        RuleFor(x => x.Note)
            .MaximumLength(500).WithMessage("Note cannot exceed 500 characters.");

        RuleFor(x => x.LastStatusUpdate)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("LastStatusUpdate must be in the past.");

        RuleFor(x => x.NextCall)
            .NotEmpty().When(x => x.Conclusion == CallResult.YenidənZəng)
            .WithMessage("NextCall is required when Conclusion is 'YenidənZəng'.");
    }
}
