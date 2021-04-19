using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleParser
{
    public partial class Vehicle
    {
        public int Id { get; set; }
        public int? OwnerId { get; set; }
        public int? VehicleModelId { get; set; }
        public string RegistrationMark { get; set; }

        public virtual User Owner { get; set; }
        public virtual VehicleModel VehicleModel { get; set; }
    }
}
