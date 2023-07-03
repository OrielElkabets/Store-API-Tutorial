using StoreServices.Data;
using StoreServices.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreServices
{
    public class UsersService
    {
        private readonly StoreDbContext _storeDbContext;

        public UsersService(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        public List<UserEO> GetUsers()
        {
            return _storeDbContext.Users.ToList();
        }

        public UserEO GetUser(int id)
        {
            return _storeDbContext.Users.Find(id);
        }

        public UserEO CreateUser(UserEO user)
        {
            _storeDbContext.Users.Add(user);
            _storeDbContext.SaveChanges();
            return user;
        }

        public UserEO UpdateUser(UserEO user)
        {
            var userToUpdate = _storeDbContext.Users.Find(user.Id);
            if (userToUpdate is null) return null;

            if (user.FName is not null)
                userToUpdate.FName = user.FName;

            if (user.LName is not null)
                userToUpdate.LName = user.LName;

            if (user.Email is not null)
                userToUpdate.Email = user.Email;

            if (user.Password is not null)
                userToUpdate.Password = user.Password;

            if (user.Role is not null)
                userToUpdate.Role = user.Role;

            _storeDbContext.SaveChanges();
            return userToUpdate;
        }

        public UserEO DeleteUser(int id)
        {
            var user = _storeDbContext.Users.Find(id);
            if (user is null) return null;
            _storeDbContext.Users.Remove(user);
            _storeDbContext.SaveChanges();
            return user;
        }

        public UserEO Auth(string email, string password)
        {
            return _storeDbContext.Users.FirstOrDefault(user => user.Email == email && user.Password == password);
        }
    }
}
