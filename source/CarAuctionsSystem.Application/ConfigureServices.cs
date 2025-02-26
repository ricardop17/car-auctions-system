using CarAuctionsSystem.Application.Interfaces;
using CarAuctionsSystem.Application.Models;
using CarAuctionsSystem.Application.Services;
using CarAuctionsSystem.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CarAuctionsSystem.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<IValidator<SearchVehicleCriteriaDto>, SearchVehicleValidator>();
        services.AddScoped<IValidator<CreateVehicleDto>, CreateVehicleValidator>();

        return services;
    }
}
