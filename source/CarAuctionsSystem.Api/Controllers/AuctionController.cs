using CarAuctionsSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarAuctionsSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuctionController : ControllerBase
{
    private readonly ILogger<AuctionController> _logger;
    private readonly IAuctionService _auctionService;

    public AuctionController(ILogger<AuctionController> logger, IAuctionService auctionService)
    {
        _logger = logger;
        _auctionService = auctionService;
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
}
