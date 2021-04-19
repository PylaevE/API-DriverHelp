using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleParser
{
    public partial class User
    {
        public User()
        {
            Events = new HashSet<Event>();
            Vehicles = new HashSet<Vehicle>();
        }

        public int Id { get; set; }
        public string MobileNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string City { get; set; }

        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
