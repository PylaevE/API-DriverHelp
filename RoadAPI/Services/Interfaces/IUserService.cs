using System.Collections.Generic;
using RoadAPI.Entities.User;

namespace RoadAPI.Services.Interfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user, string password);
        void Update(User user, string password = null);
        User Delete(int id);
    }
}
