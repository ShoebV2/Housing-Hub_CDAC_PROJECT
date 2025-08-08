using FluentValidation;
using HousingHubBackend.Dtos;
using System.Linq;

namespace HousingHubBackend.Validators
{
    public class BookingDtoValidator : AbstractValidator<BookingDto>
    {
        public BookingDtoValidator()
        {
            RuleFor(x => x.UserId).NotNull();
            RuleFor(x => x.AmenityId).NotNull();
            RuleFor(x => x.StartDate).NotNull();
            RuleFor(x => x.EndDate).NotNull();

            RuleFor(x => x.TransactionId)
                .NotEmpty().WithMessage("TransactionId is required");
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required")
                .Must(status => new[] { "Pending", "Confirmed", "Cancelled" }.Contains(status))
                .WithMessage("Invalid status value");
        }
    }

    public class CreateBookingDtoValidator : AbstractValidator<CreateBookingDto>
    {
        public CreateBookingDtoValidator()
        {
            RuleFor(x => x.UserId).NotNull();
            RuleFor(x => x.AmenityId).NotNull();
            RuleFor(x => x.StartDate).NotNull();
            RuleFor(x => x.EndDate).NotNull();

            RuleFor(x => x.TransactionId)
                .NotEmpty().WithMessage("TransactionId is required");
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required")
                .Must(status => new[] { "Pending", "Confirmed", "Cancelled" }.Contains(status))
                .WithMessage("Invalid status value");
        }
    }

    public class UpdateBookingDtoValidator : AbstractValidator<UpdateBookingDto>
    {
        public UpdateBookingDtoValidator()
        {
            RuleFor(x => x.UserId).NotNull();
            RuleFor(x => x.AmenityId).NotNull();
            RuleFor(x => x.StartDate).NotNull();
            RuleFor(x => x.EndDate).NotNull();

            RuleFor(x => x.TransactionId)
                .NotEmpty().WithMessage("TransactionId is required");
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required")
                .Must(status => new[] { "Pending", "Confirmed", "Cancelled" }.Contains(status))
                .WithMessage("Invalid status value");
        }
    }
}
