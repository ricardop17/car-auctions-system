using CarAuctionsSystem.Domain;
using CarAuctionsSystem.Domain.Entities;

namespace CarAuctionsSystem.Application.Interfaces;

public interface IVehicleRepository
{
    Task<Vehicle?> GetById(string id);
    Task<List<Vehicle>> GetAll();
    Task<List<Vehicle>> Search(string? type, string? manufacturer, string? model, int? year);
    Task<Vehicle> Create(Vehicle vehicle);
}
