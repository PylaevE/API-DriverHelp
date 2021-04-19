using NetTopologySuite.Geometries;
using RoadAPI.Entities.Enums;

namespace RoadAPI.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public EventType EventType { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public string Need { get; set; }
        public string CreateDate { get; set; }
        public Point Location { get; set; }
    }
}