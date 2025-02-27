using CarAuctionsSystem.Application.Interfaces;
using CarAuctionsSystem.Application.Models;
using CarAuctionsSystem.Domain;
using CarAuctionsSystem.Domain.Entities;
using CarAuctionsSystem.Domain.Exceptions;
using FluentValidation;

namespace CarAuctionsSystem.Application.Services;

public class AuctionService(
    IAuctionRepository auctionRepository,
    IVehicleRepository vehicleRepository
) : IAuctionService
{
    private readonly IAuctionRepository _auctionRepository = auctionRepository;
    private readonly IVehicleRepository _vehicleRepository = vehicleRepository;

    /// <summary>
    /// Get auction by id
    /// </summary>
    public async Task<Auction?> GetById(string id)
    {
        return await _auctionRepository.GetById(id);
    }

    /// <summary>
    /// Get all auctions
    /// </summary>
    /// <returns></returns>
    public async Task<List<Auction>> GetAll()
    {
        return await _auctionRepository.GetAll();
    }

    /// <summary>
    /// Start auction for vehicle
    /// </summary>
    public async Task<Auction> Start(string vehicleId)
    {
        var vehicle = await _vehicleRepository.GetById(vehicleId);

        if (vehicle is null)
        {
            throw new InvalidAuctionRequestException($"Vehicle with id {vehicleId} not found");
        }

        var anyBiddingAuction = (await _auctionRepository.GetAll()).Where(a =>
            a.Vehicle.Id == vehicleId && a.Status == AuctionStatus.Bidding
        );

        if (anyBiddingAuction.Any())
        {
            throw new InvalidAuctionRequestException(
                $"There is already an auction for vehicle with id {vehicleId}"
            );
        }

        var auction = await _auctionRepository.Create(vehicle);

        return auction;
    }

    /// <summary>
    /// Stops an auction by id
    /// </summary>
    public async Task<Auction> Stop(string auctionId)
    {
        var auction = await _auctionRepository.GetById(auctionId);

        if (auction is null)
        {
            throw new InvalidAuctionRequestException($"Auction with id {auctionId} not found");
        }

        if (auction.Status != AuctionStatus.Bidding)
        {
            throw new InvalidAuctionRequestException(
                $"Auction with id {auctionId} is not in bidding status"
            );
        }

        // update auction accordingly
        auction.EndTime = DateTime.UtcNow;
        auction.Status = auction.CurrentBidInEuros.HasValue
            ? AuctionStatus.Sold
            : AuctionStatus.Unsold;

        return await _auctionRepository.Update(auction);
    }

    /// <summary>
    /// Bids on an auction by id
    /// </summary>
    public async Task<Auction> Bid(string auctionId, PlaceBidDto bidDto)
    {
        var auction = await _auctionRepository.GetById(auctionId);

        if (auction is null)
        {
            throw new InvalidAuctionRequestException($"Auction with id {auctionId} not found");
        }

        if (auction.Status != AuctionStatus.Bidding)
        {
            throw new InvalidAuctionRequestException(
                $"Auction with id {auctionId} is not in bidding status"
            );
        }

        if (bidDto.AmountInEuros < auction.Vehicle.StartingBidInEuros)
        {
            throw new InvalidAuctionRequestException(
                $"Bid amount must be greater than starting bid amount for vehicle {auction.Vehicle.Id}"
            );
        }

        if (auction.CurrentBidInEuros.HasValue && bidDto.AmountInEuros <= auction.CurrentBidInEuros)
        {
            throw new InvalidAuctionRequestException(
                $"Bid amount must be greater than current bid amount: {auction.CurrentBidInEuros}"
            );
        }

        auction.CurrentBidInEuros = bidDto.AmountInEuros;

        return await _auctionRepository.Update(auction);
    }
}
