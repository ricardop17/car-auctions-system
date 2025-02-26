using CarAuctionsSystem.Application.Interfaces;
using CarAuctionsSystem.Domain.Entities;

namespace CarAuctionsSystem.Application.Services;

public class AuctionService(IAuctionRepository auctionRepository) : IAuctionService
{
    private readonly IAuctionRepository _auctionRepository = auctionRepository;

    public async Task<Auction?> GetById(string id)
    {
        return await _auctionRepository.GetById(id);
    }

    public async Task<List<Auction>> GetAll()
    {
        return await _auctionRepository.GetAll();
    }
}
