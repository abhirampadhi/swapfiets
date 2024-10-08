using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SF.BikeTheft.Application.Commands.Create;
using SF.BikeTheft.Application.Commands.Delete;
using SF.BikeTheft.Application.Commands.Update;
using SF.BikeTheft.Application.Queries;
using SF.BikeTheft.WebApi.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace SF.BikeTheft.WebApi.Controllers;

[ApiController]
[Route("api/bikethefts")]
public class BikeTheftsController : ControllerBase
{
    private readonly ILogger<BikeTheftsController> _logger;
    private readonly IMapper _mapper;
    private IMediator _mediator;

    public BikeTheftsController(IMediator mediator, IMapper mapper,
   ILogger<BikeTheftsController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    // GET api/bikethefts?city=Amsterdam&distance=20
    [AllowAnonymous]
    [HttpPost("search")]
    [SwaggerOperation(Summary = "Search for bike thefts in a specified city within a distance range.")]
    [ProducesResponseType(typeof(IEnumerable<Models.BikeTheft>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> SearchBikeThefts([FromQuery] string city, [FromQuery] int distance = 20)
    {
        _logger.LogInformation("BEGIN: SearchBikeThefts for city: {City}, distance: {Distance}", city, distance);

        var query = new GetBikeTheftsQuery(city, distance);

        if (query is null)
        {
            _logger.LogWarning("SearchBikeThefts query is null.");
            return BadRequest("Invalid query parameters.");
        }

        var result = await _mediator.Send(query);
        _logger.LogInformation("END: SearchBikeThefts with result count: {Count}", result?.Count());
        return Ok(result);
    }

    // GET api/bikethefts/count?city=Amsterdam&distance=20
    [HttpGet("count")]
    [SwaggerOperation(Summary = "Get the count of bike thefts in a specified city within a distance range.")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetBikeTheftCount([FromQuery] string city, [FromQuery] int distance = 20)
    {
        _logger.LogInformation("BEGIN: GetBikeTheftCount for city: {City}, distance: {Distance}", city, distance);

        var query = new GetBikeTheftsCountQuery(city, distance);

        var result = await _mediator.Send(query);
        _logger.LogInformation("END: GetBikeTheftCount with result: {Count}", result);

        return Ok(result);
    }

    // GET api/bikethefts/{id}
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Retrieve bike theft details by ID.")]
    [ProducesResponseType(typeof(Models.BikeTheft), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetBikeTheftById(int id)
    {
        _logger.LogInformation("BEGIN: GetBikeTheftById with ID: {Id}", id);

        var query = new GetBikeTheftByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
        {
            _logger.LogWarning("Bike theft with ID: {Id} not found.", id);
            return NotFound();
        }

        _logger.LogInformation("END: GetBikeTheftById with result: {@Result}", result);
        return Ok(result);
    }

    // GET: api/bikethefts/bydaterange
    [Authorize(Policy = "User")]
    [HttpGet("bydaterange")]
    [SwaggerOperation(Summary = "Get bike thefts within a specified date range.")]
    [ProducesResponseType(typeof(IEnumerable<Models.BikeTheft>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetBikeTheftsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        _logger.LogInformation("BEGIN: GetBikeTheftsByDateRange from {StartDate} to {EndDate}", startDate, endDate);

        if (startDate > endDate)
        {
            return BadRequest("Start date cannot be greater than end date.");
        }

        var query = new GetBikeTheftsByDateRangeQuery(startDate, endDate);
        var bikeThefts = await _mediator.Send(query);

        if (bikeThefts == null || !bikeThefts.Any())
        {
            _logger.LogWarning("No bike thefts found within the date range: {StartDate} to {EndDate}.", startDate, endDate);
            return NotFound("No bike thefts found within the given date range.");
        }

        _logger.LogInformation("END: GetBikeTheftsByDateRange with result count: {Count}", bikeThefts.Count());
        return Ok(bikeThefts);
    }

    // POST api/bikethefts
    [Authorize(Policy = "Admin")]
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new bike theft record.")]
    [ProducesResponseType(typeof(Models.BikeTheft), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> CreateBikeTheft([FromBody] CreateBikeTheftCommand command)
    {
        _logger.LogInformation("BEGIN: CreateBikeTheft ");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _mediator.Send(command);
        _logger.LogInformation("END: CreateBikeTheft ");
        return Ok();
    }

    // PUT api/bikethefts/{id}
    [Authorize(Policy = "Admin")]
    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Update an existing bike theft record.")]
    [ProducesResponseType(typeof(Models.BikeTheft), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponse))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> UpdateBikeTheft(int id, [FromBody] UpdateBikeTheftCommand command)
    {
        _logger.LogInformation("BEGIN: UpdateBikeTheft ");

        await _mediator.Send(command);

        _logger.LogInformation("END: UpdateBikeTheft ");
        return Ok();
    }

    // DELETE api/bikethefts/{id}
    [HttpDelete("{id}")]
    [Authorize(Policy = "Admin")]
    [SwaggerOperation(Summary = "Delete a bike theft record.")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DeleteBikeTheft(int id)
    {
        _logger.LogInformation("BEGIN: DeleteBikeTheft with ID: {Id}", id);

        var command = new DeleteBikeTheftCommand(id);
        await _mediator.Send(command);

        _logger.LogInformation("END: DeleteBikeTheft for ID: {Id}", id);
        return NoContent();
    }
}
