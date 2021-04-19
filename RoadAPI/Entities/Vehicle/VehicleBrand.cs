namespace RoadAPI.Entities.Vehicle
{
    public class VehicleBrand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public VehicleModel[] Models { get; set; }
    }
}
