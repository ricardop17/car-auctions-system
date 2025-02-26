using CarAuctionsSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarAuctionsSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class VehicleController : ControllerBase
{
    private readonly ILogger<VehicleController> _logger;
    private readonly IVehicleService _vehicleService;

    public VehicleController(ILogger<VehicleController> logger, IVehicleService vehicleService)
    {
        _logger = logger;
        _vehicleService = vehicleService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById()
    {
        var vehicle = await _vehicleService.GetById("123");
        return Ok(vehicle);
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var vehicles = await _vehicleService.GetAll();
        return Ok(vehicles);
    }
}
