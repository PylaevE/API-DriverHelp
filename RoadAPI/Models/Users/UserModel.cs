using RoadAPI.Entities;

namespace RoadAPI.Models.Users
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Car { get; set; }
        public int MarksCount { get; set; }
    }
}