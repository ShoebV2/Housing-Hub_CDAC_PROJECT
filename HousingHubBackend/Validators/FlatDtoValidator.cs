using FluentValidation;
using HousingHubBackend.Dtos;
using System.Linq;

namespace HousingHubBackend.Validators
{
    public class FlatDtoValidator : AbstractValidator<FlatDto>
    {
        public FlatDtoValidator()
        {
            RuleFor(x => x.FlatNumber)
                .NotEmpty().WithMessage("FlatNumber is required");
            RuleFor(x => x.FloorNumber)
                .NotEmpty().WithMessage("FloorNumber is required");
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required")
                .Must(status => new[] { "Occupied", "Vacant", "UnderMaintenance" }.Contains(status))
                .WithMessage("Invalid status value");
        }
    }

    public class CreateFlatDtoValidator : AbstractValidator<CreateFlatDto>
    {
        public CreateFlatDtoValidator()
        {
            RuleFor(x => x.FlatNumber)
                .NotEmpty().WithMessage("FlatNumber is required");
            RuleFor(x => x.FloorNumber)
                .NotEmpty().WithMessage("FloorNumber is required");
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required")
                .Must(status => new[] { "Occupied", "Vacant", "UnderMaintenance" }.Contains(status))
                .WithMessage("Invalid status value");
        }
    }

    public class UpdateFlatDtoValidator : AbstractValidator<UpdateFlatDto>
    {
        public UpdateFlatDtoValidator()
        {
            RuleFor(x => x.FlatNumber)
                .NotEmpty().WithMessage("FlatNumber is required");
            RuleFor(x => x.FloorNumber)
                .NotEmpty().WithMessage("FloorNumber is required");
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required")
                .Must(status => new[] { "Occupied", "Vacant", "UnderMaintenance" }.Contains(status))
                .WithMessage("Invalid status value");
        }
    }
}