using CarAuctionsSystem.Application.Interfaces;
using CarAuctionsSystem.Application.Models;
using CarAuctionsSystem.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CarAuctionsSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuctionController : ControllerBase
{
    private readonly ILogger<AuctionController> _logger;
    private readonly IAuctionService _auctionService;
    private readonly IValidator<PlaceBidDto> _bidValidator;

    public AuctionController(
        ILogger<AuctionController> logger,
        IAuctionService auctionService,
        IValidator<PlaceBidDto> bidValidator
    )
    {
        _logger = logger;
        _auctionService = auctionService;
        _bidValidator = bidValidator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(string id)
    {
        _logger.LogInformation("Getting auction by id: {id}", id);

        var auction = await _auctionService.GetById(id);

        if (auction == null)
        {
            _logger.LogWarning("Vehicle with id {id} not found", id);
            return NotFound();
        }

        _logger.LogInformation("Vehicle with id {id} found", id);

        return Ok(auction);
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        _logger.LogInformation("Getting all auctions");

        var auctions = await _auctionService.GetAll();

        _logger.LogInformation("Done getting all auctions");

        return Ok(auctions);
    }

    [HttpPost("start/{vehicleId}")]
    public async Task<ActionResult> Start(string vehicleId)
    {
        _logger.LogInformation("Starting auction for vehicle with id: {vehicleId}", vehicleId);

        var auction = await _auctionService.Start(vehicleId);

        _logger.LogInformation(
            "Finished starting auction for vehicle with id: {vehicleId}",
            vehicleId
        );

        return Ok(new ResultDto<Auction> { StatusCode = 201, Content = auction });
    }

    [HttpPut("stop/{auctionId}")]
    public async Task<ActionResult> Stop(string auctionId)
    {
        _logger.LogInformation("Stopping auction with id: {auctionId}", auctionId);

        var auction = await _auctionService.Stop(auctionId);

        _logger.LogInformation("Finished stopping auction with id: {auctionId}", auctionId);

        return Ok(new ResultDto<Auction> { StatusCode = 204, Content = auction });
    }

    [HttpPut("bid/{auctionId}")]
    public async Task<ActionResult> Bid(string auctionId, PlaceBidDto placeBidDto)
    {
        _logger.LogInformation("Placing a bid for auction with id: {auctionId}", auctionId);

        var bidValidation = await _bidValidator.ValidateAsync(placeBidDto);

        if (!bidValidation.IsValid)
        {
            _logger.LogWarning("Invalid bid data: {errors}", bidValidation.Errors);
            return BadRequest(bidValidation.Errors);
        }

        var auction = await _auctionService.Bid(auctionId, placeBidDto);

        _logger.LogInformation(
            "Finished placing a bid for auction with id: {auctionId}",
            auctionId
        );

        return Ok(new ResultDto<Auction> { StatusCode = 204, Content = auction });
    }
}
