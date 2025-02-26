using CarAuctionsSystem.Application.Helpers;
using CarAuctionsSystem.Application.Interfaces;
using CarAuctionsSystem.Application.Models;
using CarAuctionsSystem.Domain.Entities;

namespace CarAuctionsSystem.Application.Services;

/// <summary>
/// This class is responsible for handling vehicle-related operations.
/// </summary>
public class VehicleService(IVehicleRepository vehicleRepository) : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository = vehicleRepository;

    /// <summary>
    /// Retrieves a vehicle by its ID.
    /// </summary>
    public async Task<Vehicle?> GetById(string id)
    {
        return await _vehicleRepository.GetById(id);
    }

    /// <summary>
    /// Retrieves all vehicles.
    /// </summary>
    public async Task<List<Vehicle>> GetAll()
    {
        return await _vehicleRepository.GetAll();
    }

    /// <summary>
    /// Adds a new vehicle.
    /// </summary>
    public async Task<Vehicle> Add(CreateVehicleDto vehicle)
    {
        return await _vehicleRepository.Add(VehicleMapper.Convert(vehicle));
    }
}
