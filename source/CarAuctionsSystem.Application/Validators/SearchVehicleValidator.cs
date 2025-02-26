using CarAuctionsSystem.Application.Models;
using CarAuctionsSystem.Domain;
using FluentValidation;

namespace CarAuctionsSystem.Application.Validators;

public class SearchVehicleValidator : AbstractValidator<SearchVehicleCriteriaDto>
{
    public SearchVehicleValidator()
    {
        RuleFor(x => x.Type)
            .Must(type => Enum.TryParse<VehicleType>(type, true, out _))
            .When(x => !string.IsNullOrEmpty(x.Type))
            .WithMessage("Vehicle type is not valid.");
    }
}
