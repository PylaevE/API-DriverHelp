using System.Collections.Generic;
using System.Linq;
using RoadAPI.Contexts;
using RoadAPI.Entities.Vehicle;
using RoadAPI.Helpers;
using RoadAPI.Services.Interfaces;

namespace RoadAPI.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly Context _context;

        public VehicleService(Context context)
        {
            _context = context;
        }

        public void Add(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
        }

        public IEnumerable<VehicleBrand> GetVehiclesBrands()
        {
            return _context.VehicleBrands;
        }

        public IEnumerable<VehicleModel> GetVehiclesModelsByBrandId(int brandId)
        {
            return _context.VehicleModels.Where(vm => vm.Brand.Id == brandId);
        }

        public Vehicle GetVehicleById(int vehicleId)
        {
            return _context.Vehicles
                .First(v => v.Id == vehicleId);
        }

        public void Update(Vehicle updatedVehicle)
        {
            var currentVehicle = GetVehicleById(updatedVehicle.Id);

            if (currentVehicle == null)
                throw new AppException("Vehicle not found");

            if (!string.IsNullOrWhiteSpace(updatedVehicle.RegistrationMark))
                currentVehicle.RegistrationMark = updatedVehicle.RegistrationMark;
        }
    }
}
