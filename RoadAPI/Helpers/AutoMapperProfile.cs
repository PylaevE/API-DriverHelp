using AutoMapper;
using NetTopologySuite.Geometries;
using RoadAPI.Entities;
using RoadAPI.Entities.User;
using RoadAPI.Entities.Vehicle;
using RoadAPI.Models.Events;
using RoadAPI.Models.Users;
using RoadAPI.Models.Vehicles;
using VehicleModel = RoadAPI.Models.Vehicles.VehicleModel;

namespace RoadAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //User
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();
            CreateMap<User, DetailUserModel>();

            //Event
            CreateMap<CreateModel, Event>()
                .ForMember(
                    x => x.Location,
                    x => x.MapFrom(m =>
                        new Point(m.Longitude, m.Latitude){ SRID = 4326}));
            CreateMap<Event, EventModel>()
                .ForMember(
                    x => x.Longitude,
                    x => x.MapFrom(m => m.Location.X))
                .ForMember(
                    x => x.Latitude,
                    x => x.MapFrom(m => m.Location.Y));
            CreateMap<EventUpdateModel, Event>();
            
            //Vehicle
            CreateMap<VehicleBrand, VehicleBrandModel>();
            CreateMap<VehicleModel, Vehicle>();
        }
    }
}