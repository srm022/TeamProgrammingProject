using NUnit.Framework;
using PZProject.Data.Database.Entities.Group;
using PZProject.Data.Database.Entities.Note;
using PZProject.Handlers.Utils;
using System.Collections.Generic;
using System.Security;

namespace PZProject.Tests.Utils
{
    public class SecurityAssertionsTests
    {
        [Test]
        public void Should_Throw_SecurityException_If_Non_Creator_Tries_To_Manage_Group()
        {
            //ARRANGE
            var group = new GroupEntity {CreatorId = 1};
            var userId = 235453;

            //ACT && ASSERT
            Assert.Throws<SecurityException>(() => SecurityAssertions.AssertThatIssuerIsAuthorizedToManageGroup(group, userId));
        }

        [Test]
        public void Should_Throw_SecurityException_If_Non_Creator_Tries_To_Manage_Note()
        {
            //ARRANGE
            var note = new NoteEntity { CreatorId = 1 };
            var userId = 235453;

            //ACT && ASSERT
            Assert.Throws<SecurityException>(() => SecurityAssertions.AssertThatIssuerIsAuthorizedToManageNote(note, userId));
        }

        [TestCase(1, 1)]
        [TestCase(1, 10)]
        [TestCase(10, 1)]
        public void Should_Throw_SecurityException_If_Non_Creator_Tries_To_Delete_Note(int groupCreatorId, int noteCreatorId)
        {
            //ARRANGE
            var group = new GroupEntity { CreatorId = groupCreatorId };
            var note = new NoteEntity { CreatorId = noteCreatorId };
            var userId = 20;

            //ACT && ASSERT
            Assert.Throws<SecurityException>(() => SecurityAssertions.AssertThatIssuerIsAuthorizedToDeleteNote(note, group, userId));
        }

        [TestCase(1, 1)]
        [TestCase(1, 10)]
        [TestCase(10, 1)]
        public void Group_Creator_Should_Be_Able_To_Delete_Note(int groupCreatorId, int noteCreatorId)
        {
            //ARRANGE
            var note = new NoteEntity { CreatorId = noteCreatorId };
            var group = new GroupEntity { CreatorId = groupCreatorId };
            var userId = groupCreatorId;

            //ACT && ASSERT
            Assert.DoesNotThrow(() => SecurityAssertions.AssertThatIssuerIsAuthorizedToDeleteNote(note, group, userId));
        }

        [Test]
        public void Should_Throw_SecurityException_If_User_Does_Not_Belong_To_Group()
        {
            //ARRANGE
            var testSubjectUserId = 3;
            var groupId = 10;
            var group = new GroupEntity
            {
                GroupId = groupId,
                UserGroups = new List<UserGroupEntity>
                {
                    new UserGroupEntity
                    {
                        GroupId = groupId,
                        UserId = 1
                    },
                    new UserGroupEntity
                    {
                        GroupId = groupId,
                        UserId = 2
                    }
                }
            };

            //ACT && ASSERT
            Assert.Throws<SecurityException>(() => SecurityAssertions.AssertThatUserBelongsToGroup(group, testSubjectUserId));
        }
    }
}