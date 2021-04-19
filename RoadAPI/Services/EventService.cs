using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NetTopologySuite.Geometries;
using RoadAPI.Contexts;
using RoadAPI.Entities;
using RoadAPI.Entities.Enums;
using RoadAPI.Helpers;
using RoadAPI.Services.Interfaces;

namespace RoadAPI.Services
{
    public class EventService : IEventService
    {
        private readonly Context _context;

        public EventService(Context context)
        {
            _context = context;
        }

        public void Create(Event @event, int authorId)
        {
            @event.AuthorId = authorId;
            @event.CreateDate = DateTime.UtcNow.ToString(CultureInfo.CurrentCulture);
            _context.Add(@event);
            _context.SaveChanges();
        }

        public void Update(Event updatedEvent)
        {
            var currentEvent = GetEventById(updatedEvent.Id);
            if (currentEvent == null)
                throw new AppException("Event not found");
            
            if (!string.IsNullOrWhiteSpace(updatedEvent.Description))
                currentEvent.Description = updatedEvent.Description;
            
            if (!string.IsNullOrWhiteSpace(updatedEvent.Need))
                currentEvent.Need = updatedEvent.Need;

            _context.Events.Update(currentEvent);
            _context.SaveChanges();
        }
        
        public Event Delete(int eventId)
        {
            var @event = GetEventById(eventId);
            if (@event != null)
            {
                _context.Remove(@event);
                _context.SaveChanges();
            }
            return @event;
        }


        public IEnumerable<Event> GetAll(int limit)
        {
            return _context.Events.Take(limit);
        }

        public Event GetEventById(int id)
        {
            return _context.Events.FirstOrDefault(@event => @event.Id == id);
        }
        
        public IEnumerable<Event> GetInRadius(double latitude, double longitude, double radius, EventType eventTypes)
        {//TODO: ВСЁ РАВНО ЭТО ПЛОХО, НАДО ДОБАВИТЬ ФИЛЬТР СНАЧАЛА
            var eventTypesList = GetEventTypes(eventTypes);
            var point = new Point(longitude, latitude){SRID = 4326};
            var result = _context.Events
                .Where(x => eventTypesList.Contains(x.EventType))
                .Select(x => Tuple.Create(x, x.Location.MetersDistance(point)))
                .ToList()
                .Where(x => x.Item2 <= radius)
                .Select(x => x.Item1);
            return result;
        }

        private static List<EventType> GetEventTypes(EventType eventTypes)
        {
            var result = new List<EventType>();
            foreach (var type in Enum.GetValues(typeof(EventType)))
                if (eventTypes.HasFlag((EventType)type))
                    result.Add((EventType)type);
            return result;
        }
    }
}