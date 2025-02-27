using CarAuctionsSystem.Domain.Entities;

namespace CarAuctionsSystem.Application.Interfaces;

public interface IAuctionService
{
    Task<Auction?> GetById(string id);
    Task<List<Auction>> GetAll();
    Task<Auction> Start(string vehicleId);
}
