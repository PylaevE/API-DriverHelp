namespace RoadAPI.Entities.Vehicle
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public User.User Owner { get; set; }
        public VehicleModel VehicleModel { get; set; }
        public int VehicleModelId { get; set; }
        public string RegistrationMark { get; set; }
    }
}
