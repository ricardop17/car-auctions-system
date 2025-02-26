using CarAuctionsSystem.Application.Interfaces;
using CarAuctionsSystem.Domain.Entities;

namespace CarAuctionsSystem.Application.Services;

public class VehicleService(IVehicleRepository vehicleRepository) : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository = vehicleRepository;

    public Task<Vehicle?> GetById(string id)
    {
        return _vehicleRepository.GetById(id);
    }

    public Task<List<Vehicle>> GetAll()
    {
        return _vehicleRepository.GetAll();
    }

    public Task Add(Vehicle vehicle)
    {
        return _vehicleRepository.Add(vehicle);
    }
}
