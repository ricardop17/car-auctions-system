using CarAuctionsSystem.Application.Interfaces;
using CarAuctionsSystem.Application.Models;
using CarAuctionsSystem.Application.Services;
using CarAuctionsSystem.Domain;
using CarAuctionsSystem.Domain.Entities;
using CarAuctionsSystem.Domain.Exceptions;
using CarAuctionsSystem.Tests.Fixtures;
using Microsoft.Extensions.Logging;
using Moq;

namespace CarAuctionsSystem.Tests.Services;

public class TestAuctionService
{
    private readonly Mock<ILogger<AuctionService>> _mockLogger;
    private readonly Mock<IAuctionRepository> _mockAuctionRepository;
    private readonly Mock<IVehicleRepository> _mockVehicleRepository;
    private readonly AuctionService _service;

    public TestAuctionService()
    {
        _mockLogger = new Mock<ILogger<AuctionService>>();
        _mockAuctionRepository = new Mock<IAuctionRepository>();
        _mockVehicleRepository = new Mock<IVehicleRepository>();
        _service = new AuctionService(
            _mockLogger.Object,
            _mockAuctionRepository.Object,
            _mockVehicleRepository.Object
        );
    }

    [Fact]
    public async Task GetById_OK()
    {
        // Arrange
        var auction = new Auction(new Suv());
        _mockAuctionRepository.Setup(x => x.GetById(It.IsAny<string>())).ReturnsAsync(auction);

        // Act
        var result = await _service.GetById("1");

        // Assert
        Assert.Equal(auction, result);
    }

    [Fact]
    public async Task GetAll_OK()
    {
        // Arrange
        var auctions = AuctionFixtures.GetAuctions();
        _mockAuctionRepository.Setup(x => x.GetAll()).ReturnsAsync(auctions);

        // Act
        var result = await _service.GetAll();

        // Assert
        Assert.Equal(auctions, result);
    }

    [Fact]
    public async Task StartAuction_OK()
    {
        // Arrange
        var auction = AuctionFixtures.GetAuctions().First();
        var vehicle = VehicleFixtures.GetVehicles().First();

        _mockVehicleRepository.Setup(x => x.GetById(It.IsAny<string>())).ReturnsAsync(vehicle);
        _mockAuctionRepository.Setup(x => x.GetAll()).ReturnsAsync([]);
        _mockAuctionRepository.Setup(x => x.Create(It.IsAny<Vehicle>())).ReturnsAsync(auction);

        // Act
        var result = await _service.Start("vehicleId");

        // Assert
        Assert.Equal(auction, result);
    }

    [Fact]
    public async Task StartAuction_Vehicle_NotFound()
    {
        // Arrange
        _mockVehicleRepository
            .Setup(x => x.GetById(It.IsAny<string>()))
            .ReturnsAsync((Vehicle?)null);

        // Act
        async Task result() => await _service.Start("vehicleId");

        // Assert
        var exception = await Assert.ThrowsAsync<InvalidAuctionRequestException>(result);
        Assert.Equal("Vehicle with id vehicleId not found", exception.Message);
    }

    [Fact]
    public async Task StartAuction_Action_Already_Set_For_Vehicle()
    {
        // Arrange
        var requestVehicleId = "vehicleId";
        var vehicle = VehicleFixtures.GetVehicles().First();
        vehicle.Id = requestVehicleId;

        var auction = AuctionFixtures.GetAuctions().First();
        auction.Vehicle = vehicle;
        auction.Status = AuctionStatus.Bidding;

        _mockVehicleRepository.Setup(x => x.GetById(It.IsAny<string>())).ReturnsAsync(vehicle);
        _mockAuctionRepository.Setup(x => x.GetAll()).ReturnsAsync([auction]);

        // Act
        async Task result() => await _service.Start(requestVehicleId);

        // Assert
        var exception = await Assert.ThrowsAsync<InvalidAuctionRequestException>(result);
        Assert.Equal(
            $"There is already an auction for vehicle with id {requestVehicleId}",
            exception.Message
        );
    }

    [Fact]
    public async Task StopAuction_OK()
    {
        // Arrange
        var auction = AuctionFixtures.GetAuctions().First();

        _mockAuctionRepository.Setup(x => x.GetById(It.IsAny<string>())).ReturnsAsync(auction);
        _mockAuctionRepository.Setup(x => x.Update(It.IsAny<Auction>())).ReturnsAsync(auction);

        // Act
        var result = await _service.Stop("vehicleId");

        // Assert
        Assert.Equal(auction, result);
    }

    [Fact]
    public async Task StopAuction_Auction_NotFound()
    {
        // Arrange
        _mockAuctionRepository
            .Setup(x => x.GetById(It.IsAny<string>()))
            .ReturnsAsync((Auction?)null);

        // Act
        async Task result() => await _service.Stop("vehicleId");

        // Assert
        var exception = await Assert.ThrowsAsync<InvalidAuctionRequestException>(result);
        Assert.Equal($"Auction with id vehicleId not found", exception.Message);
    }

    [Fact]
    public async Task StopAuction_Auction_Invalid_Status()
    {
        // Arrange
        var auction = AuctionFixtures.GetAuctions().First();
        auction.Status = AuctionStatus.Sold;

        _mockAuctionRepository.Setup(x => x.GetById(It.IsAny<string>())).ReturnsAsync(auction);

        // Act
        async Task result() => await _service.Stop(auction.Id);

        // Assert
        var exception = await Assert.ThrowsAsync<InvalidAuctionRequestException>(result);
        Assert.Equal($"Auction with id {auction.Id} is not in bidding status", exception.Message);
    }

    [Fact]
    public async Task BindAuction_OK()
    {
        // Arrange
        var auction = AuctionFixtures.GetAuctions().First();

        _mockAuctionRepository.Setup(x => x.GetById(It.IsAny<string>())).ReturnsAsync(auction);
        _mockAuctionRepository.Setup(x => x.Update(It.IsAny<Auction>())).ReturnsAsync(auction);

        // Act
        var result = await _service.Bid(
            "vehicleId",
            new PlaceBidDto() { AmountInEuros = auction.Vehicle.StartingBidInEuros + 1 }
        );

        // Assert
        Assert.Equal(auction, result);
    }

    [Fact]
    public async Task BindAuction_Auction_Not_Found()
    {
        // Arrange
        _mockAuctionRepository
            .Setup(x => x.GetById(It.IsAny<string>()))
            .ReturnsAsync((Auction?)null);

        // Act
        async Task result() =>
            await _service.Bid("vehicleId", new PlaceBidDto() { AmountInEuros = 1 });

        // Assert
        var exception = await Assert.ThrowsAsync<InvalidAuctionRequestException>(result);
        Assert.Equal("Auction with id vehicleId not found", exception.Message);
    }

    [Fact]
    public async Task BindAuction_Auction_Invalid_State()
    {
        // Arrange
        var auction = AuctionFixtures.GetAuctions().First();
        auction.Status = AuctionStatus.Unsold;

        _mockAuctionRepository.Setup(x => x.GetById(It.IsAny<string>())).ReturnsAsync(auction);

        // Act
        async Task result() =>
            await _service.Bid(auction.Id, new PlaceBidDto() { AmountInEuros = 1 });

        // Assert
        var exception = await Assert.ThrowsAsync<InvalidAuctionRequestException>(result);
        Assert.Equal($"Auction with id {auction.Id} is not in bidding status", exception.Message);
    }

    [Fact]
    public async Task BindAuction_Less_Than_StartingBid_For_Vehicle()
    {
        // Arrange
        var auction = AuctionFixtures.GetAuctions().First();

        _mockAuctionRepository.Setup(x => x.GetById(It.IsAny<string>())).ReturnsAsync(auction);

        // Act
        async Task result() =>
            await _service.Bid(
                "vehicleId",
                new PlaceBidDto() { AmountInEuros = auction.Vehicle.StartingBidInEuros - 1 }
            );

        // Assert
        var exception = await Assert.ThrowsAsync<InvalidAuctionRequestException>(result);
        Assert.Equal(
            $"Bid amount must be greater than starting bid amount for vehicle {auction.Vehicle.Id}",
            exception.Message
        );
    }

    [Fact]
    public async Task BindAuction_Less_Or_Equal_Than_Current_Bid()
    {
        // Arrange
        var auction = AuctionFixtures.GetAuctions().First();
        auction.CurrentBidInEuros = auction.Vehicle.StartingBidInEuros + 1000;

        _mockAuctionRepository.Setup(x => x.GetById(It.IsAny<string>())).ReturnsAsync(auction);

        // Act
        async Task result() =>
            await _service.Bid(
                "vehicleId",
                new PlaceBidDto() { AmountInEuros = auction.CurrentBidInEuros!.Value }
            );

        // Assert
        var exception = await Assert.ThrowsAsync<InvalidAuctionRequestException>(result);
        Assert.Equal(
            $"Bid amount must be greater than current bid amount: {auction.CurrentBidInEuros}",
            exception.Message
        );
    }
}
