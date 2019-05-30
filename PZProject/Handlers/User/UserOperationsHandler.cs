using AutoMapper;
using PZProject.Data.Database.Entities.User;
using PZProject.Data.Repositories.User;
using PZProject.Data.Requests.UserRequests;
using PZProject.Data.Responses;
using PZProject.Handlers.Utils;
using System;

namespace PZProject.Handlers.User
{
    public interface IUserOperationsHandler
    {
        void RegisterNewUser(RegisterUserRequest request);
        LoginUserResponse LoginUser(LoginUserRequest request);
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

        public void RegisterNewUser(RegisterUserRequest request)
        {
            VerifyEmailAvailability(request.Email);
            PasswordHashHandler.CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);
            var user = MapRequestToEntity(request, passwordHash, passwordSalt);
            Register(user);
        }

        public LoginUserResponse LoginUser(LoginUserRequest request)
        {
            var user = GetUserByEmail(request.Email);
            if (PasswordHashHandler.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                var token = _tokenGenerator.GenerateJwtToken(user.UserId, user.Role.Name);
                return new LoginUserResponse(token, user.UserId);
            }

            throw new Exception("Wrong credentials");
        }

        private UserEntity GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        private void VerifyEmailAvailability(string email)
        {
            _userRepository.VerifyIfUserExistsForEmail(email);
        }

        private void Register(UserEntity userEntity)
        {
            _userRepository.CreateUser(userEntity);
        }

        private UserEntity MapRequestToEntity(RegisterUserRequest request, byte[] passwordHash, byte[] passwordSalt)
        {
            var user = Mapper.Map<UserEntity>(request);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return user;
        }
    }
}