using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleParser
{
    public partial class VehicleBrand
    {
        public VehicleBrand()
        {
            VehicleModels = new HashSet<VehicleModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<VehicleModel> VehicleModels { get; set; }
    }
}
