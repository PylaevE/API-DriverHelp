using System.Collections.Generic;
using RoadAPI.Entities;
using RoadAPI.Entities.Enums;

namespace RoadAPI.Services.Interfaces
{
    public interface IEventService
    {
        void Create(Event @event, int authorId);
        IEnumerable<Event> GetAll(int limit);
        Event GetEventById(int id);
        IEnumerable<Event> GetInRadius(double latitude, double longitude, double radius, EventType eventTypes);
        Event Delete(int id);
        void Update(Event @event);
    }
}
