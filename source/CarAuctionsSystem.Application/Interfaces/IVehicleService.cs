using CarAuctionsSystem.Application.Models;
using CarAuctionsSystem.Domain.Entities;

namespace CarAuctionsSystem.Application.Interfaces;

public interface IVehicleService
{
    Task<List<Vehicle>> GetAll();
    Task<Vehicle?> GetById(string id);
    Task<Vehicle> Add(CreateVehicleDto vehicle);
}
