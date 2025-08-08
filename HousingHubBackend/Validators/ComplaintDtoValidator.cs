using FluentValidation;
using HousingHubBackend.Dtos;

namespace HousingHubBackend.Validators
{
    public class ComplaintDtoValidator : AbstractValidator<ComplaintDto>
    {
        public ComplaintDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required");
        }
    }

    public class CreateComplaintDtoValidator : AbstractValidator<CreateComplaintDto>
    {
        public CreateComplaintDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required");
        }
    }

    public class UpdateComplaintDtoValidator : AbstractValidator<UpdateComplaintDto>
    {
        public UpdateComplaintDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required");
        }
    }
}
