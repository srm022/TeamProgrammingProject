using Microsoft.EntityFrameworkCore;
using PZProject.Data.Database;
using PZProject.Data.Database.Entities.User;
using System;
using System.Linq;

namespace PZProject.Data.Repositories.User
{
    public interface IUserRepository
    {
        void CreateUser(UserEntity userEntity);
        void VerifyIfUserExistsForEmail(string email);
        UserEntity GetUserByEmail(string email);
        UserEntity GetUserById(int id);
    }

    public class UserRepository : IUserRepository
    {
        private readonly SystemDbContext _db;

        public UserRepository(SystemDbContext db)
        {
            _db = db;
        }

        public void CreateUser(UserEntity userEntity)
        {
            userEntity.Role = _db.Roles.SingleOrDefault(r => r.Name == "User");
            _db.Users.Add(userEntity);
            SaveChanges();
        }

        public void VerifyIfUserExistsForEmail(string email)
        {
            if (_db.Users.Any(x => x.Email == email))
                throw new Exception($"Email {email} is already taken");
        }

        public UserEntity GetUserByEmail(string email)
        {
            var user = _db.Users
                .Include(r => r.Role)
                .SingleOrDefault(u => u.Email == email);
                
            if (user == null) throw new Exception("User not found"); //todo

            return user;
        }

        public UserEntity GetUserById(int id)
        {
            var user = _db.Users
                .Include(r => r.Role)
                .SingleOrDefault(u => u.UserId == id);

            if (user == null) throw new Exception("User not found"); //todo

            return user;
        }

        private void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}