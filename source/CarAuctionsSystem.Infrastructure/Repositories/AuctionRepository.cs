using CarAuctionsSystem.Application.Interfaces;
using CarAuctionsSystem.Domain.Entities;

namespace CarAuctionsSystem.Infrastructure.Repositories;

public class AuctionRepository : IAuctionRepository
{
    private readonly List<Auction> _auctions = [];

    public Task<Auction?> GetById(string id)
    {
        return Task.FromResult(_auctions.FirstOrDefault(a => a.Id == id));
    }

    public Task<List<Auction>> GetAll()
    {
        return Task.FromResult(_auctions);
    }
}
