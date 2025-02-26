using CarAuctionsSystem.Application.Interfaces;
using CarAuctionsSystem.Application.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CarAuctionsSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class VehicleController : ControllerBase
{
    private readonly ILogger<VehicleController> _logger;
    private readonly IVehicleService _vehicleService;
    private readonly IValidator<CreateVehicleDto> _createValidator;
    private readonly IValidator<SearchVehicleCriteriaDto> _searchValidator;

    public VehicleController(
        ILogger<VehicleController> logger,
        IVehicleService vehicleService,
        IValidator<CreateVehicleDto> createValidator,
        IValidator<SearchVehicleCriteriaDto> searchValidator
    )
    {
        _logger = logger;
        _vehicleService = vehicleService;
        _createValidator = createValidator;
        _searchValidator = searchValidator;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(string id)
    {
        _logger.LogInformation("Getting vehicle by id: {id}", id);

        var vehicle = await _vehicleService.GetById(id);

        if (vehicle == null)
        {
            _logger.LogWarning("Vehicle with id {id} not found", id);
            return NotFound();
        }

        _logger.LogInformation("Vehicle with id {id} found", id);

        return Ok(vehicle);
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        _logger.LogInformation("Getting all vehicles");

        var vehicles = await _vehicleService.GetAll();

        _logger.LogInformation("Done getting all vehicles");

        return Ok(vehicles);
    }

    [HttpPost("search")]
    public async Task<ActionResult> Search([FromBody] SearchVehicleCriteriaDto criteria)
    {
        _logger.LogInformation("Search for vehicles");

        var validationResult = _searchValidator.Validate(criteria);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Invalid search criteria");
            return BadRequest(validationResult.Errors);
        }

        var vehicles = await _vehicleService.Search(criteria);

        _logger.LogInformation("Done searching vehicles");

        return Ok(vehicles);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateVehicleDto createVehicleDto)
    {
        _logger.LogInformation("Starting to create a new vehicle");

        var validationResult = _createValidator.Validate(createVehicleDto);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Invalid vehicle data");
            return BadRequest(validationResult.Errors);
        }

        var vehicle = await _vehicleService.Create(createVehicleDto);

        if (vehicle == null)
        {
            _logger.LogWarning("Failed to create a new vehicle");
            return BadRequest();
        }

        _logger.LogInformation("Successfully created a new vehicle");

        return Ok(vehicle.Id);
    }
}
