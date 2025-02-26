using CarAuctionsSystem.Domain.Entities;

namespace CarAuctionsSystem.Application.Interfaces
{
    public interface IAuctionRepository
    {
        List<Auction> GetAll();
    }
}
