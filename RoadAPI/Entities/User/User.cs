using System.Collections.Generic;
using System.ComponentModel;

namespace RoadAPI.Entities.User
{
    public class User
    {
        public int Id { get; set; }
        [DefaultValue(Enums.Role.User)]
        public string Role { get; set; }
        public string MobileNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string City { get; set; }
        public List<Vehicle.Vehicle> Vehicles { get; set; }
        public List<Event> Events { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public int MarksCount
        {
            get => Events.Count;
        }
    }
}