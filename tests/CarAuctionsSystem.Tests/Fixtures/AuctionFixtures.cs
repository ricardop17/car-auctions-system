using CarAuctionsSystem.Domain.Entities;

namespace CarAuctionsSystem.Tests.Fixtures;

internal static class AuctionFixtures
{
    internal static List<Auction> GetAuctions() =>
        [
            new Auction(
                new Suv()
                {
                    Id = "123",
                    Manufacturer = "Toyota",
                    Model = "Yaris",
                    Year = 2021,
                    NumberOfSeats = 5,
                    StartingBidInEuros = 10000
                }
            ),
            new Auction(
                new Sedan()
                {
                    Id = "456",
                    Manufacturer = "Ford",
                    Model = "Focus",
                    Year = 2020,
                    NumberOfDoors = 4,
                    StartingBidInEuros = 20000
                }
            )
        ];
}
