using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RoadAPI.Contexts;
using RoadAPI.Entities.User;
using RoadAPI.Helpers;
using RoadAPI.Services.Interfaces;

namespace RoadAPI.Services
{
    public class UserService : IUserService
    {
        private readonly Context _context;
        
        public UserService(Context context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Username == username);
            
            if (user == null)
                return null;
            
            return VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt) ? user : null;
        }
        
        public IEnumerable<User> GetAll()
        {
            return _context.Users
                .Include(c => c.Events);
        }

        public User GetById(int id)
        {
            return _context.Users
                .Include(c => c.Events)
                .FirstOrDefault(user => user.Id == id);
        }

        public User Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");
            
            if (_context.Users.Any(x => x.Username == user.Username))
                throw new AppException("Username \"" + user.Username + "\" is already taken");
            
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(User updatedUser, string password = null)
        {
            var currentUser = GetById(updatedUser.Id);
            if (currentUser == null)
                throw new AppException("User not found");

            // update username if it has changed
            if (!string.IsNullOrWhiteSpace(updatedUser.Username) && updatedUser.Username != currentUser.Username)
            {
                if (_context.Users.Any(x => x.Username == updatedUser.Username))
                    throw new AppException("Username " + updatedUser.Username + " is already taken");

                currentUser.Username = updatedUser.Username;
            }
            
            // update mobile number if it has changed
            if (!string.IsNullOrWhiteSpace(updatedUser.MobileNumber) && updatedUser.MobileNumber != currentUser.MobileNumber)
            {
                if (_context.Users.Any(x => x.MobileNumber == updatedUser.MobileNumber))
                    throw new AppException("Mobile number " + updatedUser.MobileNumber + " is already taken");

                currentUser.MobileNumber = updatedUser.MobileNumber;
            }
            
            if (!string.IsNullOrWhiteSpace(updatedUser.FirstName))
                currentUser.FirstName = updatedUser.FirstName;

            if (!string.IsNullOrWhiteSpace(updatedUser.LastName))
                currentUser.LastName = updatedUser.LastName;
            
            if (!string.IsNullOrWhiteSpace(updatedUser.City))
                currentUser.City = updatedUser.City;
            
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                currentUser.PasswordHash = passwordHash;
                currentUser.PasswordSalt = passwordSalt;
            }
            
            _context.Users.Update(currentUser);
            _context.SaveChanges();
        }

        public User Delete(int id)
        {
            var user = GetById(id);

            if (user != null)
            {
                _context.Remove(user);
                _context.SaveChanges();
            }

            return user;
        }
        
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).");

            using var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != storedHash[i]) return false;
            }
            
            hmac.Dispose();
            return true;
        }
    }
}