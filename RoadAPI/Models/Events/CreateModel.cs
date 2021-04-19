using System.ComponentModel.DataAnnotations;
using RoadAPI.Entities.Enums;

namespace RoadAPI.Models.Events
{
    public class CreateModel
    {
        [Required]
        public EventType EventType { get; set; }
        [Required]
        public string Description { get; set; }
        public string Need { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
    }
}