using CarAuctionsSystem.Application.Interfaces;
using CarAuctionsSystem.Domain.Entities;

namespace CarAuctionsSystem.Infrastructure.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private List<Vehicle> _vehicles =
    [
        new Hatchback
        {
            Id = "123",
            Manufacturer = "Ford",
            Model = "Fiesta",
            Year = 2020,
            StartingBidInEuros = 8000,
            NumberOfDoors = 5
        },
        new Sedan
        {
            Id = "456",
            Manufacturer = "BMW",
            Model = "M70",
            Year = 2017,
            StartingBidInEuros = 8000,
            NumberOfDoors = 5
        },
        new Suv
        {
            Id = "789",
            Manufacturer = "Audi",
            Model = "Q7",
            Year = 2018,
            StartingBidInEuros = 8000,
            NumberOfSeats = 5
        },
        new Truck
        {
            Id = "333",
            Manufacturer = "Volvo",
            Model = "FH16",
            Year = 2017,
            StartingBidInEuros = 8000,
            LoadCapacityKg = 20000
        },
        new Hatchback
        {
            Id = "334",
            Manufacturer = "Volvo",
            Model = "FH17",
            Year = 2024,
            StartingBidInEuros = 7000,
            NumberOfDoors = 3
        },
    ];

    public Task<Vehicle?> GetById(string id)
    {
        return Task.FromResult(_vehicles.FirstOrDefault(v => v.Id == id));
    }

    public Task<List<Vehicle>> GetAll()
    {
        return Task.FromResult(_vehicles);
    }

    public Task<List<Vehicle>> Search(string? type, string? manufacturer, string? model, int? year)
    {
        var result = _vehicles
            .Where(v =>
                (type is null || v.Type.ToString() == type)
                && (year is null || v.Year == year)
                && (manufacturer is null || v.Manufacturer == manufacturer)
                && (model is null || v.Model == model)
            )
            .ToList();

        return Task.FromResult(result);
    }

    public Task<Vehicle> Create(Vehicle vehicle)
    {
        _vehicles.Add(vehicle);
        return Task.FromResult(vehicle);
    }
}
