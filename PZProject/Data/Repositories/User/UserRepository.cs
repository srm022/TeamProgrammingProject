using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PZProject.Data.Database;

namespace PZProject.Data.Repositories.User
{
    public interface IUserRepository
    {
        void CreateUser(Database.Entities.User.User userEntity);
        void VerifyIfUserExistsForEmail(string email);
        Database.Entities.User.User GetUserByEmail(string email);
    }

    public class UserRepository : IUserRepository
    {
        private readonly SystemDbContext _db;

        public UserRepository(SystemDbContext db)
        {
            _db = db;
        }

        public void CreateUser(Database.Entities.User.User userEntity)
        {
            userEntity.Role = _db.Roles.SingleOrDefault(r => r.Name == "Petitioner");
            _db.Users.Add(userEntity);
            SaveChanges();
        }

        public void VerifyIfUserExistsForEmail(string email)
        {
            if (_db.Users.Any(x => x.Email == email))
                throw new Exception($"Email {email} is already taken");
        }

        public Database.Entities.User.User GetUserByEmail(string email)
        {
            var user = _db.Users
                .Include(r => r.Role)
                .SingleOrDefault(u => u.Email == email);
                
            if (user == null) throw new Exception("User not found"); //todo

            return user;
        }

        private void SaveChanges()
        {
            _db.SaveChanges();
        }
    }

    public class UserSecretsModel
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}