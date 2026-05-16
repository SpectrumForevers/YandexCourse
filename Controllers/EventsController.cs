using EventManagementApi.Dtos;
using EventManagementApi.Models;
using EventManagementApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementApi.Controllers;

[ApiController]
[Route("events")]
public class EventsController : ControllerBase
{
    private readonly IEventService eventService;

    public EventsController(IEventService eventService)
    {
        this.eventService = eventService;
    }

    [HttpGet]
    public ActionResult<IReadOnlyCollection<Event>> GetAll()
    {
        IReadOnlyCollection<Event> events = eventService.GetAll();

        return Ok(events);
    }

    [HttpGet("{eventId:guid}")]
    public ActionResult<Event> GetById(Guid eventId)
    {
        Event? eventItem = eventService.GetById(eventId);

        if (eventItem is null)
        {
            return NotFound();
        }

        return Ok(eventItem);
    }

    [HttpPost]
    public ActionResult<Event> Create(CreateEventRequest request)
    {
        Event eventToCreate = new Event
        {
            Title = request.Title!,
            Description = request.Description,
            StartAt = request.StartAt!.Value,
            EndAt = request.EndAt!.Value
        };

        Event createdEvent = eventService.Create(eventToCreate);

        return CreatedAtAction(
            nameof(GetById),
            new { eventId = createdEvent.Id },
            createdEvent);
    }

    [HttpPut("{eventId:guid}")]
    public IActionResult Update(Guid eventId, UpdateEventRequest request)
    {
        Event updatedEvent = new Event
        {
            Title = request.Title!,
            Description = request.Description,
            StartAt = request.StartAt!.Value,
            EndAt = request.EndAt!.Value
        };

        bool isUpdated = eventService.Update(eventId, updatedEvent);

        if (!isUpdated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{eventId:guid}")]
    public IActionResult Delete(Guid eventId)
    {
        bool isDeleted = eventService.Delete(eventId);

        if (!isDeleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}