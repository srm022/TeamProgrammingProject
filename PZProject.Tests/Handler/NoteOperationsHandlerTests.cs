using System;
using PZProject.Tests.Infrastructure;
using Moq;
using NUnit.Framework;
using PZProject.Tests.Infrastructure;
using PZProject.Data.Database.Entities.User;
using PZProject.Handlers.Note;
using PZProject.Handlers.Group.Operations.CreateNote;
using PZProject.Data.Repositories.User;
using PZProject.Data.Repositories.Note;
using PZProject.Handlers.Note.Operations.Edit;
using PZProject.Handlers.Note.Operations.Delete;
using PZProject.Data.Requests.NoteRequests;
using PZProject.Data.Repositories.Group;
using PZProject.Data.Database.Entities.Group;
using PZProject.Data.Database.Entities.Note;

namespace PZProject.Tests.Handler
{
    public class NoteOperationsHandlerTests : TestsInfrastructure
    {
        [Test]
        public void Should_Throw_Exception_For_Invalid_IssuerId([Values(-1, 0, null)] int invalidIssuerId)
        {
            //ARRANGE
            var fixture = new NoteOperationsHandlerTestsFixture()
                .ConfigureSut();

            var request = CreateValidCreateNoteRequest();
            var groupId = 86;

            //ACT && ASSERT
            var exception = Assert.Throws<Exception>(() => fixture.Sut.CreateNoteForGroup(request, groupId, invalidIssuerId));
            Assert.That(exception.Message, Is.EqualTo($"Could not find entity of type [{typeof(UserEntity).Name}]"));
        }

        [Test]
        public void Should_Create_Note_For_Valid_Create_Input_Parameters()
        {
            //ARRANGE
            var issuerId = 100;
            var groupId = 100;
            var fixture = new NoteOperationsHandlerTestsFixture()
                .SetupUserRepositoryToReturnUserForId(issuerId)
                .SetupGroupRepositoryToReturnGroupForId(groupId)
                .ConfigureSut();

            var request = CreateValidCreateNoteRequest();

            //ACT && ASSERT
            Assert.DoesNotThrow(() => fixture.Sut.CreateNoteForGroup(request, groupId, issuerId));
        }

        [Test]
        public void Should_Throw_Exception_For_Invalid_GroupId([Values(-1, 0, null)] int invalidGroupId)
        {
            //ARRANGE
            var issuerId = 100;
            var fixture = new NoteOperationsHandlerTestsFixture()
                .SetupUserRepositoryToReturnUserForId(issuerId)
                .ConfigureSut();

            var request = CreateValidCreateNoteRequest();

            //ACT && ASSERT
            var exception = Assert.Throws<Exception>(() => fixture.Sut.CreateNoteForGroup(request, invalidGroupId, issuerId));
            Assert.That(exception.Message, Is.EqualTo($"Could not find entity of type [{typeof(GroupEntity).Name}]"));
        }

        [Test]
        public void Should_Throw_Exception_For_Invalid_NoteId([Values(-1, 0, null)] int invalidNoteId)
        {
            //ARRANGE
            var issuerId = 100;
            var editRequest = new EditNoteRequest
            {
                NoteId = invalidNoteId,
                NoteName = "noteName",
                NoteDescription = "NoteDescription"
            };
            var fixture = new NoteOperationsHandlerTestsFixture()
                .SetupUserRepositoryToReturnUserForId(issuerId)
                .ConfigureSut();
                
            //ACT && ASSERT
            var exception = Assert.Throws<Exception>(() => fixture.Sut.EditNote(editRequest, issuerId));
            Assert.That(exception.Message, Is.EqualTo($"Could not find entity of type [{typeof(NoteEntity).Name}]"));
        }

        private CreateNoteRequest CreateValidCreateNoteRequest()
        {
            return new CreateNoteRequest
            {
                NoteName = "noteName",
                NoteDescription = "noteDescription"
            };
        }
    }

    public class  NoteOperationsHandlerTestsFixture
    {
        public NoteOperationsHandler Sut { get; set; }
        public int ValidIssuerId { get; set; }

        private readonly Mock<INoteCreator> _noteCreatorMock = new Mock<INoteCreator>();
        private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
        private readonly Mock<INoteRepository> _noteRepositoryMock = new Mock<INoteRepository>();
        private readonly Mock<INoteEditHandler> _noteEditHandlerMock = new Mock<INoteEditHandler>();
        private readonly Mock<INoteDeleteHandler> _noteDeleteHandlerMock = new Mock<INoteDeleteHandler>();
        private readonly Mock<IGroupRepository> _groupRepositoryMock = new Mock<IGroupRepository>();

        public NoteOperationsHandlerTestsFixture ConfigureSut()
        {
            Sut = new NoteOperationsHandler(_noteRepositoryMock.Object,
                _noteCreatorMock.Object,
                _noteEditHandlerMock.Object,
                _noteDeleteHandlerMock.Object,
                _userRepositoryMock.Object,
                _groupRepositoryMock.Object);

            return this;
        }

        public NoteOperationsHandlerTestsFixture SetupUserRepositoryToReturnUserForId(int userId)
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

        public NoteOperationsHandlerTestsFixture SetupGroupRepositoryToReturnGroupForId(int groupId)
        {
            _groupRepositoryMock
                .Setup(r => r.GetGroupById(It.IsAny<int>()))
                .Returns(new GroupEntity
                {
                    GroupId = groupId,
                    Name = "TestGroup",
                    Description = "TestGroupDescription"
                });

            return this;
        }
    }
}