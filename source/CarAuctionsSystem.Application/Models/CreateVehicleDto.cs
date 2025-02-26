namespace CarAuctionsSystem.Application.Models;

public record CreateVehicleDto
{
    public required string Type { get; set; }
    public required string Manufacturer { get; set; }
    public required string Model { get; set; }
    public int Year { get; set; }
    public decimal StartingBidInEuros { get; set; }
    public int NumberOfDoors { get; set; }
    public int NumberOfSeats { get; set; }
    public decimal LoadCapacityInKg { get; set; }
}
