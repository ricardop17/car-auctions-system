using CarAuctionsSystem.Application.Models;
using CarAuctionsSystem.Domain.Entities;

namespace CarAuctionsSystem.Application.Interfaces;

public interface IVehicleService
{
    Task<Vehicle?> GetById(string id);
    Task<List<Vehicle>> GetAll();
    Task<List<Vehicle>> Search(SearchVehicleCriteriaDto criteria);
    Task<Vehicle> Add(CreateVehicleDto vehicle);
}
