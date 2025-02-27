using CarAuctionsSystem.Application.Interfaces;
using CarAuctionsSystem.Application.Models;
using CarAuctionsSystem.Application.Services;
using CarAuctionsSystem.Domain.Entities;
using CarAuctionsSystem.Tests.Fixtures;
using Microsoft.Extensions.Logging;
using Moq;

namespace CarAuctionsSystem.Tests.Services;

public class TestVehicleService
{
    private readonly Mock<ILogger<VehicleService>> _mockLogger;
    private readonly Mock<IVehicleRepository> _mockVehicleRepository;
    private readonly VehicleService _service;

    public TestVehicleService()
    {
        _mockLogger = new Mock<ILogger<VehicleService>>();
        _mockVehicleRepository = new Mock<IVehicleRepository>();
        _service = new VehicleService(_mockLogger.Object, _mockVehicleRepository.Object);
    }

    [Fact]
    public async Task GetById_OK()
    {
        // Arrange
        var vehicle = new Suv();
        _mockVehicleRepository.Setup(x => x.GetById(It.IsAny<string>())).ReturnsAsync(vehicle);

        // Act
        var result = await _service.GetById("123");

        // Assert
        Assert.Equal(vehicle, result);
    }

    [Fact]
    public async Task GetALL_OK()
    {
        // Arrange
        List<Vehicle> vehicles = VehicleFixtures.GetVehicles();
        _mockVehicleRepository.Setup(x => x.GetAll()).ReturnsAsync(vehicles);

        // Act
        var result = await _service.GetAll();

        // Assert
        Assert.Equal(vehicles, result);
    }

    [Fact]
    public async Task Search_OK()
    {
        // Arrange
        List<Vehicle> vehicles = VehicleFixtures.GetVehicles();

        var request = new SearchVehicleCriteriaDto()
        {
            Manufacturer = "Toyota",
            Model = "Corolla",
            Type = "Sedan",
            Year = 2020
        };

        _mockVehicleRepository
            .Setup(x =>
                x.Search(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<int>()
                )
            )
            .ReturnsAsync(vehicles);

        // Act
        var result = await _service.Search(request);

        // Assert
        Assert.Equal(vehicles, result);
    }

    [Fact]
    public async Task Create_OK()
    {
        // Arrange
        var request = new CreateVehicleDto()
        {
            Manufacturer = "Toyota",
            Model = "Corolla",
            Type = "Sedan",
            Year = 2020
        };

        var vehicle = VehicleFixtures.GetVehicles().First();

        _mockVehicleRepository.Setup(x => x.Create(It.IsAny<Vehicle>())).ReturnsAsync(vehicle);

        // Act
        var result = await _service.Create(request);

        // Assert
        Assert.Equal(vehicle, result);
    }
}
