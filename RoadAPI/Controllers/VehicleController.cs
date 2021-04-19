using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadAPI.Entities.Vehicle;
using RoadAPI.Helpers;
using RoadAPI.Models.Vehicles;
using RoadAPI.Services.Interfaces;
using VehicleModel = RoadAPI.Models.Vehicles.VehicleModel;

namespace RoadAPI.Controllers
{
    [Authorize(AuthenticationSchemes="Bearer")]
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;

        public VehicleController(IVehicleService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            _mapper = mapper;
        }

        [HttpGet("GetVehiclesBrands")]
        public IActionResult GetVehiclesBrands()
        {
            var brands = _vehicleService.GetVehiclesBrands();
            var model = _mapper.Map<IList<VehicleBrandModel>>(brands);
            return Ok(model);
        }
        
        [HttpGet("GetVehicleModels")]
        public IActionResult GetVehicleModels(int brandId)
        {
            var models = _vehicleService.GetVehiclesModelsByBrandId(brandId);
            return Ok(models);
        }
        
        [HttpPost("AddVehicle")]
        public IActionResult AddVehicle([FromBody]VehicleModel model)
        {
            var authorId = int.Parse(HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Name)
                ?.Value!);
            var vehicle = _mapper.Map<Vehicle>(model);
            vehicle.OwnerId = authorId;
            try
            {
                _vehicleService.Add(vehicle);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
