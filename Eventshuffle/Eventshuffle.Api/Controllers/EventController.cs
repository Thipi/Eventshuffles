using Eventshuffle.Application;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/v1/[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateEvent([FromBody]CreateEventRequest request)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var eventDto = await _eventService.CreateEventAsync(request);
        return Ok(new { íd = eventDto.Id });
    }

    [HttpGet("list")]
    public async Task<IActionResult> ListEvents()
    {
        var events = await _eventService.GetEventsAsync();
        return Ok(new { events });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(int id)
    {
        var eventDto = await _eventService.GetEventByIdAsync(id);
        if (eventDto == null) return NotFound();

        return Ok(eventDto);
    }

    [HttpPost("vote")]
    public async Task<IActionResult> AddVote([FromBody] CreateVoteRequest request)
    {
        var result = await _eventService.AddVoteAsync(request);
        if (!result) return NotFound();

        return Ok();
    }

    [HttpGet("{id}/results")]
    public async Task<IActionResult> GetResults(int id)
    {
        var results = await _eventService.GetEventResultsAsync(id);
        if (results == null) return NotFound();

        return Ok(new { suitableDates = results });
    }
}