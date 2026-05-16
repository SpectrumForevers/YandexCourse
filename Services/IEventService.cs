using EventManagementApi.Models;

namespace EventManagementApi.Services;

public interface IEventService
{
    IReadOnlyCollection<Event> GetAll();

    Event? GetById(Guid eventId);

    Event Create(Event eventToCreate);

    bool Update(Guid eventId, Event updatedEvent);

    bool Delete(Guid eventId);
}