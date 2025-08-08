using FluentValidation;
using HousingHubBackend.Dtos.Maintenance;

namespace HousingHubBackend.Validators
{
    public class MaintenanceBillDtoValidator : AbstractValidator<MaintenanceBillDto>
    {
        public MaintenanceBillDtoValidator()
        {
            RuleFor(x => x.FlatId)
                .NotNull().WithMessage("FlatId is required");
            RuleFor(x => x.Amount)
                .NotNull().WithMessage("Amount is required");
            RuleFor(x => x.TransactionId)
                .NotEmpty().WithMessage("TransactionId is required");
            RuleFor(x => x.PeriodStart)
                .NotNull().WithMessage("PeriodStart is required");
            RuleFor(x => x.PeriodEnd)
                .NotNull().WithMessage("PeriodEnd is required");
            RuleFor(x => x.DueDate)
                .NotNull().WithMessage("DueDate is required");
        }
    }

    public class MaintenanceFeeDtoValidator : AbstractValidator<MaintenanceFeeDto>
    {
        public MaintenanceFeeDtoValidator()
        {
            RuleFor(x => x.WingId)
                .NotNull().WithMessage("WingId is required");
            RuleFor(x => x.RatePerSqft)
                .NotNull().WithMessage("RatePerSqft is required");
            RuleFor(x => x.EffectiveFrom)
                .NotNull().WithMessage("EffectiveFrom is required");
            RuleFor(x => x.EffectiveTo)
                .NotNull().WithMessage("EffectiveTo is required");
        }
    }
}