using CarAuctionsSystem.Application.Models;
using FluentValidation;

namespace CarAuctionsSystem.Application.Validators;

public class PlaceBidValidator : AbstractValidator<PlaceBidDto>
{
    public PlaceBidValidator()
    {
        RuleFor(x => x.AmountInEuros).GreaterThan(0);
    }
}
