namespace CarAuctionsSystem.Domain.Entities;

public sealed class Truck : Vehicle
{
    public Truck()
    {
        Type = VehicleType.Truck;
    }

    public decimal LoadCapacityKg { get; set; }
}
