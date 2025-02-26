using CarAuctionsSystem.Application.Interfaces;
using CarAuctionsSystem.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CarAuctionsSystem.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IVehicleService, VehicleService>();

        return services;
    }
}
