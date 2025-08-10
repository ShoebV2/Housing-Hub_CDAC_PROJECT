using FluentValidation;
using HousingHubBackend.Dtos;

namespace HousingHubBackend.Validators
{
    public class WingDtoValidator : AbstractValidator<WingDto>
    {
        public WingDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.TotalFloors)
                .NotNull().WithMessage("TotalFloors is required");
            RuleFor(x => x.FlatsPerFloor)
                .NotNull().WithMessage("FlatsPerFloor is required");
            RuleFor(x => x.SocietyId)
                .NotNull().WithMessage("SocietyId is required");
        }
    }

    public class CreateWingDtoValidator : AbstractValidator<CreateWingDto>
    {
        public CreateWingDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.TotalFloors)
                .NotNull().WithMessage("TotalFloors is required");
            RuleFor(x => x.FlatsPerFloor)
                .NotNull().WithMessage("FlatsPerFloor is required");
            RuleFor(x => x.SocietyId)
                .NotNull().WithMessage("SocietyId is required");
        }
    }

    public class UpdateWingDtoValidator : AbstractValidator<UpdateWingDto>
    {
        public UpdateWingDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.TotalFloors)
                .NotNull().WithMessage("TotalFloors is required");
            RuleFor(x => x.FlatsPerFloor)
                .NotNull().WithMessage("FlatsPerFloor is required");
            RuleFor(x => x.SocietyId)
                .NotNull().WithMessage("SocietyId is required");
        }
    }
}