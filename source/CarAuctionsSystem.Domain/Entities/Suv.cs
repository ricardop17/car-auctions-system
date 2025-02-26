namespace CarAuctionsSystem.Domain.Entities;

public sealed class Suv : Vehicle
{
    public Suv()
    {
        Type = VehicleType.SUV;
    }

    public int NumberOfSeats { get; set; }
}
