using CarAuctionsSystem.Domain;

namespace CarAuctionsSystem.Application.Models;

public class SearchVehicleCriteriaDto
{
    public string? Type { get; set; }
    public string? Manufacturer { get; set; }
    public string? Model { get; set; }
    public int? Year { get; set; }
}
