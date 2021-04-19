using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleParser
{
    public partial class VehicleModel
    {
        public VehicleModel()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? BrandId { get; set; }

        public virtual VehicleBrand Brand { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
