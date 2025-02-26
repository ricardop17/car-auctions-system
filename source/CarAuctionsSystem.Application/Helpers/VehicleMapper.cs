using CarAuctionsSystem.Application.Models;
using CarAuctionsSystem.Domain;
using CarAuctionsSystem.Domain.Entities;

namespace CarAuctionsSystem.Application.Helpers;

/// <summary>
/// This class is responsible for mapping vehicle creation requests to vehicle entities.
/// </summary>
public static class VehicleMapper
{
    /// <summary>
    /// Converts a request for vehicle creation to a vehicle entity based on the type.
    /// </summary>
    public static Vehicle Convert(CreateVehicleDto request)
    {
        var parsedType = Enum.Parse<VehicleType>(request.Type);

        return parsedType switch
        {
            VehicleType.Hatchback => CreateHatchback(request),
            VehicleType.Sedan => CreateSedan(request),
            VehicleType.SUV => CreateSuv(request),
            VehicleType.Truck => CreateTruck(request),
            _ => throw new ArgumentException("Invalid vehicle type")
        };
    }

    private static Hatchback CreateHatchback(CreateVehicleDto request)
    {
        return new Hatchback()
        {
            Manufacturer = request.Manufacturer,
            Model = request.Model,
            Year = request.Year,
            StartingBidInEuros = request.StartingBidInEuros,
            NumberOfDoors = request.NumberOfDoors,
        };
    }

    private static Sedan CreateSedan(CreateVehicleDto request)
    {
        return new Sedan()
        {
            Manufacturer = request.Manufacturer,
            Model = request.Model,
            Year = request.Year,
            StartingBidInEuros = request.StartingBidInEuros,
            NumberOfDoors = request.NumberOfDoors,
        };
    }

    private static Suv CreateSuv(CreateVehicleDto request)
    {
        return new Suv()
        {
            Manufacturer = request.Manufacturer,
            Model = request.Model,
            Year = request.Year,
            StartingBidInEuros = request.StartingBidInEuros,
            NumberOfSeats = request.NumberOfSeats,
        };
    }

    private static Truck CreateTruck(CreateVehicleDto request)
    {
        return new Truck()
        {
            Manufacturer = request.Manufacturer,
            Model = request.Model,
            Year = request.Year,
            StartingBidInEuros = request.StartingBidInEuros,
            LoadCapacityKg = request.LoadCapacityInKg,
        };
    }
}
