namespace CarAuctionsSystem.Domain.Entities;

public sealed class Sedan : Vehicle
{
    public Sedan()
    {
        Type = VehicleType.Sedan;
    }

    public int NumberOfDoors { get; set; }
}
