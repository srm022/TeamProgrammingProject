using AutoMapper;
using PZProject.Data.Repositories.User;
using PZProject.Handlers.User.Model;
using PZProject.Handlers.Utils;

namespace PZProject.Handlers.User
{
    public interface IUserOperationsHandler
    {
        void RegisterNewUser(RegisterUserModel model);
        string LoginUser(LoginUserModel model);
    }

    public class UserOperationsHandler : IUserOperationsHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public UserOperationsHandler(IUserRepository userRepository,
            IJwtTokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        public void RegisterNewUser(RegisterUserModel model)
        {
            VerifyEmailAvailability(model.Email);
            PasswordHashHandler.CreatePasswordHash(model.Password, out var passwordHash, out var passwordSalt);
            var user = MapModelToEntity(model, passwordHash, passwordSalt);
            Register(user);
        }

        public string LoginUser(LoginUserModel model)
        {
            var user = GetUserByEmail(model.Email);
            if (PasswordHashHandler.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                return _tokenGenerator.GenerateJwtToken(user.UserId, user.Role.Name);
            }

            return null;
        }

        private Data.Database.Entities.User.User GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        private void VerifyEmailAvailability(string email)
        {
            _userRepository.VerifyIfUserExistsForEmail(email);
        }

        private void Register(Data.Database.Entities.User.User user)
        {
            _userRepository.CreateUser(user);
        }

        private Data.Database.Entities.User.User MapModelToEntity(RegisterUserModel model, byte[] passwordHash, byte[] passwordSalt)
        {
            var user = Mapper.Map<Data.Database.Entities.User.User>(model);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return user;
        }
    }
}