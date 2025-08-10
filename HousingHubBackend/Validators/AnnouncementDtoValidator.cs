using FluentValidation;
using HousingHubBackend.Dtos;

namespace HousingHubBackend.Validators
{
    public class AnnouncementDtoValidator : AbstractValidator<AnnouncementDto>
    {
        public AnnouncementDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required");
        }
    }

    public class CreateAnnouncementDtoValidator : AbstractValidator<CreateAnnouncementDto>
    {
        public CreateAnnouncementDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required");
        }
    }

    public class UpdateAnnouncementDtoValidator : AbstractValidator<UpdateAnnouncementDto>
    {
        public UpdateAnnouncementDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required");
        }
    }
}
