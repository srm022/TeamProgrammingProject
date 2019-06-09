using System;
using PZProject.Tests.Infrastructure;
using Moq;
using NUnit.Framework;
using PZProject.Data.Database.Entities.User;
using PZProject.Data.Repositories.User;
using PZProject.Data.Requests.UserRequests;
using PZProject.Handlers.User;
using PZProject.Handlers.Utils;

namespace PZProject.Tests.Handler
{
    public class UserOperationsHandlerTests : TestsInfrastructure
    {
        [Test]
        public void Should_Throw_Exception_For_Invalid_Password([Values("1", "2", null)] string invalidPassword)
        {
            //ARRANGE
            var fixture = new UserOperationsHandlerTestsFixture()
                .ConfigureSut();

            var request = new LoginUserRequest
            {
                Email = "email",
                Password = invalidPassword
            };

            //ACT && ASSERT
            var exception = Assert.Throws<Exception>(() => fixture.Sut.LoginUser(request));
            Assert.That(exception.Message, Is.EqualTo($"Wrong credentials"));
        }

        private RegisterUserRequest CreateValidCreateUserRequest()
        {
            return new RegisterUserRequest
            {
                Email = "email",
                Password = "Password"
            };
        }
    }

    public class UserOperationsHandlerTestsFixture
    {
        public UserOperationsHandler Sut { get; set; }
        public int ValidIssuerId { get; set; }

        private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
        private readonly Mock<IJwtTokenGenerator> _tokenGeneratorMock = new Mock<IJwtTokenGenerator>();

        public UserOperationsHandlerTestsFixture ConfigureSut()
        {
            Sut = new UserOperationsHandler(_userRepositoryMock.Object,
                _tokenGeneratorMock.Object);

            return this;
        }

        public UserOperationsHandlerTestsFixture SetupUserRepositoryToReturnUserForId(int userId)
        {
            _userRepositoryMock
                .Setup(r => r.GetUserById(It.IsAny<int>()))
                .Returns(new UserEntity
                {
                    UserId = userId,
                    FirstName = "TestUser",
                    LastName = "LastName"
                });

            return this;
        }
    }
}
