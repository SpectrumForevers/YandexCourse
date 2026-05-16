using EventManagementApi.Models;

namespace EventManagementApi.Services;

public class EventService : IEventService
{
    private readonly List<Event> events = new();

    public IReadOnlyCollection<Event> GetAll()
    {
        return events.AsReadOnly();
    }

    public Event? GetById(Guid eventId)
    {
        return events.FirstOrDefault(eventItem => eventItem.Id == eventId);
    }

    public Event Create(Event eventToCreate)
    {
        eventToCreate.Id = Guid.NewGuid();

        events.Add(eventToCreate);

        return eventToCreate;
    }

    public bool Update(Guid eventId, Event updatedEvent)
    {
        Event? existingEvent = GetById(eventId);

        if (existingEvent is null)
        {
            return false;
        }

        existingEvent.Title = updatedEvent.Title;
        existingEvent.Description = updatedEvent.Description;
        existingEvent.StartAt = updatedEvent.StartAt;
        existingEvent.EndAt = updatedEvent.EndAt;

        return true;
    }

    public bool Delete(Guid eventId)
    {
        Event? existingEvent = GetById(eventId);

        if (existingEvent is null)
        {
            return false;
        }

        events.Remove(existingEvent);

        return true;
    }
}