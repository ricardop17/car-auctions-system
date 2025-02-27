using CarAuctionsSystem.Application.Helpers;
using CarAuctionsSystem.Application.Interfaces;
using CarAuctionsSystem.Application.Models;
using CarAuctionsSystem.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CarAuctionsSystem.Application.Services;

/// <summary>
/// This class is responsible for handling vehicle-related operations.
/// </summary>
public class VehicleService(ILogger<VehicleService> logger, IVehicleRepository vehicleRepository)
    : IVehicleService
{
    private readonly ILogger<VehicleService> _logger = logger;
    private readonly IVehicleRepository _vehicleRepository = vehicleRepository;

    /// <summary>
    /// Retrieves a vehicle by its ID.
    /// </summary>
    public async Task<Vehicle?> GetById(string id)
    {
        _logger.LogDebug("Retrieving vehicle with ID {id}", id);

        return await _vehicleRepository.GetById(id);
    }

    /// <summary>
    /// Retrieves all vehicles.
    /// </summary>
    public async Task<List<Vehicle>> GetAll()
    {
        _logger.LogDebug("Retrieving all vehicles");

        return await _vehicleRepository.GetAll();
    }

    /// <summary>
    /// Search for vehicles based on criteria.
    /// </summary>
    public async Task<List<Vehicle>> Search(SearchVehicleCriteriaDto criteria)
    {
        _logger.LogDebug("Searching for vehicles with criteria");

        return await _vehicleRepository.Search(
            criteria.Type,
            criteria.Manufacturer,
            criteria.Model,
            criteria.Year
        );
    }

    /// <summary>
    /// Adds a new vehicle.
    /// </summary>
    public async Task<Vehicle> Create(CreateVehicleDto vehicle)
    {
        _logger.LogDebug("Creating a new vehicle");

        return await _vehicleRepository.Create(VehicleMapper.Convert(vehicle));
    }
}
