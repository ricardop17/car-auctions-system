using CarAuctionsSystem.Application.Models;
using CarAuctionsSystem.Application.Validators;

namespace CarAuctionsSystem.Tests.Validators;

public class TestSearchVehicleValidator
{
    private readonly SearchVehicleValidator _validator;

    public TestSearchVehicleValidator()
    {
        _validator = new SearchVehicleValidator();
    }

    [Fact]
    public void Validate_OK()
    {
        // Arrange
        var request = new SearchVehicleCriteriaDto()
        {
            Manufacturer = "Toyota",
            Model = "Corolla",
            Type = "Sedan",
            Year = 2020
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_InvalidType()
    {
        // Arrange
        var request = new SearchVehicleCriteriaDto()
        {
            Manufacturer = "Toyota",
            Model = "Corolla",
            Type = "RandomType",
            Year = 2020
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Equal("Vehicle type is not valid.", result.Errors[0].ErrorMessage);
    }
}
