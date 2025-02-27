using CarAuctionsSystem.Application.Models;
using CarAuctionsSystem.Domain.Entities;

namespace CarAuctionsSystem.Application.Interfaces;

public interface IAuctionService
{
    Task<Auction?> GetById(string id);
    Task<List<Auction>> GetAll();
    Task<Auction> Start(string vehicleId);
    Task<Auction> Stop(string auctionId);
    Task<Auction> Bid(string auctionId, PlaceBidDto bidDto);
}
