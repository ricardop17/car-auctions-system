using CarAuctionsSystem.Domain.Entities;

namespace CarAuctionsSystem.Tests.Fixtures;

internal static class VehicleFixtures
{
    internal static List<Vehicle> GetVehicles() =>
        [
            new Suv()
            {
                Id = "123",
                Manufacturer = "Toyota",
                Model = "Yaris",
                Year = 2021,
                NumberOfSeats = 5
            },
            new Sedan()
            {
                Id = "456",
                Manufacturer = "Ford",
                Model = "Focus",
                Year = 2020,
                NumberOfDoors = 4
            },
            new Hatchback()
            {
                Id = "789",
                Manufacturer = "Volkswagen",
                Model = "Golf",
                Year = 2019,
                NumberOfDoors = 5
            },
            new Truck()
            {
                Id = "101112",
                Manufacturer = "Jeep",
                Model = "Cherokee",
                Year = 2018,
                LoadCapacityKg = 1000
            }
        ];
}
