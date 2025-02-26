using CarAuctionsSystem.Application.Models;
using CarAuctionsSystem.Domain;
using FluentValidation;

namespace CarAuctionsSystem.Application.Validators;

public class CreateVehicleValidator : AbstractValidator<CreateVehicleDto>
{
    public CreateVehicleValidator()
    {
        RuleFor(x => x.Manufacturer)
            .NotNull()
            .NotEmpty()
            .WithMessage("Manufacturer name is required.");

        RuleFor(x => x.Model).NotNull().NotEmpty().WithMessage("Model name is required.");

        RuleFor(x => x.Manufacturer)
            .MaximumLength(50)
            .WithMessage("Manufacturer name is too long.");

        RuleFor(x => x.Model).MaximumLength(50).WithMessage("Model name is too long.");

        RuleFor(x => x.Year)
            .InclusiveBetween(1900, DateTime.Now.Year)
            .WithMessage("Year is not valid.");

        RuleFor(x => x.StartingBidInEuros)
            .GreaterThan(0)
            .WithMessage("Starting bid must be greater than 0.");

        RuleFor(x => x.Type)
            .NotNull()
            .NotEmpty()
            .Must(type => Enum.TryParse<VehicleType>(type, true, out _))
            .WithMessage("Type is not valid.");
    }
}
