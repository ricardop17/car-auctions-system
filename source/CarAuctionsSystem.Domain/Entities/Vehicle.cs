namespace CarAuctionsSystem.Domain.Entities;

public abstract class Vehicle
{
    public string Id { get; set; }
    public VehicleType Type { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal StartingBidEuros { get; set; }

    public Vehicle()
    {
        Id = string.Empty;
        Manufacturer = string.Empty;
        Model = string.Empty;
    }
}
