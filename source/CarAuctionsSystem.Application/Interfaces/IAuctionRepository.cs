using CarAuctionsSystem.Domain.Entities;

namespace CarAuctionsSystem.Application.Interfaces
{
    public interface IAuctionRepository
    {
        Task<Auction?> GetById(string id);
        Task<List<Auction>> GetAll();
        Task<Auction> Create(Vehicle vehicle);
    }
}
