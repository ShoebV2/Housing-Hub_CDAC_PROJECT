using FluentValidation;
using HousingHubBackend.Dtos;

namespace HousingHubBackend.Validators
{
    public class AmenityDtoValidator : AbstractValidator<AmenityDto>
    {
        public AmenityDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required");
        }
    }

    public class CreateAmenityDtoValidator : AbstractValidator<CreateAmenityDto>
    {
        public CreateAmenityDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required");
        }
    }

    public class UpdateAmenityDtoValidator : AbstractValidator<UpdateAmenityDto>
    {
        public UpdateAmenityDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required");
        }
    }
}
