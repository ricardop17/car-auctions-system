namespace CarAuctionsSystem.Domain.Entities;

public abstract class Vehicle
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public VehicleType Type { get; set; }
    public string Manufacturer { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal StartingBidInEuros { get; set; }

    public Vehicle()
    {
        Manufacturer = string.Empty;
        Model = string.Empty;
    }
}
