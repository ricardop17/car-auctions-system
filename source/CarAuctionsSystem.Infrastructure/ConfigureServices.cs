using CarAuctionsSystem.Application.Interfaces;
using CarAuctionsSystem.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CarAuctionsSystem.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSingleton<IVehicleRepository, VehicleRepository>();

        return services;
    }
}
