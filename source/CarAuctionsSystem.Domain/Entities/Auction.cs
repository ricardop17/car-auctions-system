namespace CarAuctionsSystem.Domain.Entities;

public class Auction
{
    public Guid Id { get; set; }
    public Guid VehicleId { get; set; }
    public decimal Bid { get; set; }

    public Auction(Guid vehicleId)
    {
        Id = Guid.NewGuid();
        VehicleId = vehicleId;
        Bid = 0;
    }
}
