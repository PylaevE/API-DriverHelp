using System.Collections.Generic;
using RoadAPI.Entities.Vehicle;

namespace RoadAPI.Services.Interfaces
{
    public interface IVehicleService
    {
        void Add(Vehicle vehicle);
        IEnumerable<VehicleBrand> GetVehiclesBrands();
        IEnumerable<VehicleModel> GetVehiclesModelsByBrandId(int brandId);
        Vehicle GetVehicleById(int vehicleId);
        void Update(Vehicle updatedVehicle);
    }
}
