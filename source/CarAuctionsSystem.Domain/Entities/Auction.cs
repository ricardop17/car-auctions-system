namespace CarAuctionsSystem.Domain.Entities;

public class Auction(Vehicle vehicle)
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public Vehicle Vehicle { get; set; } = vehicle;
    public decimal? CurrentBidInEuros { get; set; }

    public DateTime StartTime { get; set; } = DateTime.UtcNow;
    public DateTime? EndTime { get; set; } = null;
    public AuctionStatus Status { get; set; } = AuctionStatus.Bidding;
}
