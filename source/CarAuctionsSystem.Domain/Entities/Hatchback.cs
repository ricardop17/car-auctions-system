namespace CarAuctionsSystem.Domain.Entities;

public sealed class Hatchback : Vehicle
{
    public int NumberOfDoors { get; set; }

    public Hatchback()
    {
        Type = VehicleType.Hatchback;
    }
}
