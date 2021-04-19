using RoadAPI.Entities.Enums;

namespace RoadAPI.Models.Events
{
    public class EventModel
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public EventType EventType { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public string Need { get; set; }
        public string CreateDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}