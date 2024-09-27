using MediatR;
using Microsoft.AspNetCore.Mvc;
using SF.BikeTheft.Application.Queries;

namespace ABC.BikeTheft.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BikeTheftsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BikeTheftsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET api/bikethefts?city=Amsterdam&distance=20
    [HttpGet]
    public async Task<IActionResult> GetBikeThefts([FromQuery] string city, [FromQuery] int distance = 20)
    {
        var query = new GetBikeTheftsQuery { City = city, Distance = distance };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // GET api/bikethefts/count?city=Amsterdam&distance=20
    [HttpGet("count")]
    public async Task<IActionResult> GetBikeTheftCount([FromQuery] string city, [FromQuery] int distance = 20)
    {
        var query = new GetBikeTheftsCountQuery { City = city, Distance = distance };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // GET api/bikethefts/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBikeTheftById(int id)
    {
        var query = new GetBikeTheftByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    // GET: api/bikethefts/bydaterange
    [HttpGet("bydaterange")]
    public async Task<IActionResult> GetBikeTheftsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        if (startDate > endDate)
        {
            return BadRequest("Start date cannot be greater than end date.");
        }

        var query = new GetBikeTheftsByDateRangeQuery(startDate, endDate);
        var bikeThefts = await _mediator.Send(query);

        if (bikeThefts == null || !bikeThefts.Any())
        {
            return NotFound("No bike thefts found within the given date range.");
        }

        return Ok(bikeThefts);
    }

    // POST api/bikethefts
    [HttpPost]
    public async Task<IActionResult> CreateBikeTheft([FromBody] CreateBikeTheftCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetBikeTheftById), new { id = result.Id }, result);
    }

    // PUT api/bikethefts/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBikeTheft(int id, [FromBody] UpdateBikeTheftCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Id in the URL and in the body don't match.");
        }

        var result = await _mediator.Send(command);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    // DELETE api/bikethefts/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBikeTheft(int id)
    {
        var command = new DeleteBikeTheftCommand { Id = id };
        var result = await _mediator.Send(command);

        if (result == false)
        {
            return NotFound();
        }

        return NoContent();
    }
}
