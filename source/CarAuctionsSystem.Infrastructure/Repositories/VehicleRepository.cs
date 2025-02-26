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
            Year = 2017,
            StartingBidEuros = 8000,
            NumberOfDoors = 5
        },
        new Sedan
        {
            Id = "456",
            Manufacturer = "BMW",
            Model = "M70",
            Year = 2017,
            StartingBidEuros = 8000,
            NumberOfDoors = 5
        },
        new Suv
        {
            Id = "789",
            Manufacturer = "Audi",
            Model = "Q7",
            Year = 2017,
            StartingBidEuros = 8000,
            NumberOfSeats = 5
        },
        new Truck
        {
            Id = "333",
            Manufacturer = "Volvo",
            Model = "FH16",
            Year = 2017,
            StartingBidEuros = 8000,
            LoadCapacityKg = 20000
        }
    ];

    public Task<List<Vehicle>> GetAll()
    {
        return Task.FromResult(_vehicles);
    }

    public Task<Vehicle?> GetById(string id)
    {
        return Task.FromResult(_vehicles.FirstOrDefault(v => v.Id == id));
    }

    public Task Add(Vehicle vehicle)
    {
        _vehicles.Add(vehicle);
        return Task.CompletedTask;
    }
}
