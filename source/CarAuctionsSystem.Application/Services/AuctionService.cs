using CarAuctionsSystem.Application.Interfaces;
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
            throw new InvalidStartAuctionRequestException($"Vehicle with id {vehicleId} not found");
        }

        var anyBiddingAuction = (await _auctionRepository.GetAll()).Where(a =>
            a.Vehicle.Id == vehicleId && a.Status == AuctionStatus.Bidding
        );

        if (anyBiddingAuction.Any())
        {
            throw new InvalidStartAuctionRequestException(
                $"There is already an auction for vehicle with id {vehicleId}"
            );
        }

        var auction = await _auctionRepository.Create(vehicle);

        return auction;
    }
}
