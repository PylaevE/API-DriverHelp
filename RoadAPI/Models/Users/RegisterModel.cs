using System.ComponentModel.DataAnnotations;

namespace RoadAPI.Models.Users
{
    public class RegisterModel
    {
        [Required]
        public string MobileNumber { get; set; }
        
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string City { get; set; }
    }
}