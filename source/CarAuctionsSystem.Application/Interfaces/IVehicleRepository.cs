using CarAuctionsSystem.Domain.Entities;

namespace CarAuctionsSystem.Application.Interfaces;

public interface IVehicleRepository
{
    Task<List<Vehicle>> GetAll();
    Task<Vehicle?> GetById(string id);
    Task<Vehicle> Add(Vehicle vehicle);
}
