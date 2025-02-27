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

    public Task<Auction> Create(Vehicle vehicle)
    {
        var auction = new Auction(vehicle);

        _auctions.Add(auction);

        return Task.FromResult(auction);
    }

    public Task<Auction> Update(Auction updatedAuction)
    {
        var index = _auctions.FindIndex(a => a.Id == updatedAuction.Id);

        if (index == -1)
            throw new InvalidOperationException($"Auction with id {updatedAuction.Id} not found");

        _auctions[index] = updatedAuction;
        return Task.FromResult(updatedAuction);
    }
}
