using PZProject.Data.Database.Entities;
using PZProject.Data.Database.Entities.Group;
using PZProject.Data.Repositories.Group;
using PZProject.Handlers.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PZProject.Handlers.Group.Operations.AssignUser
{
    public interface IGroupAssignUserHandler
    {
        void AssignUserToGroup(string groupName, int userId, int issuerId);
    }

    public class GroupAssignUserHandler : IGroupAssignUserHandler
    {
        private readonly IGroupRepository _groupRepository;

        public GroupAssignUserHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public void AssignUserToGroup(string groupName, int userId, int issuerId)
        {
            var group = GetGroupForName(groupName);
            AssertThatRequestCameFromCreator(group, issuerId);
            AssertThatUserIsNotAlreadyAssigned(group.UserGroups, userId);

            AssignToGroup(group, userId);
        }

        private void AssignToGroup(GroupEntity group, int userId)
        {
            _groupRepository.AssignUserToGroup(userId, group.GroupId);
        }

        private GroupEntity GetGroupForName(string groupName)
        {
            var group = _groupRepository.GetGroupByName(groupName);
            group.AssertThatExists();

            return group;
        }

        private void AssertThatUserIsNotAlreadyAssigned(List<UserGroupEntity> userGroups, int userId)
        {
            if (userGroups.Any(ug => ug.UserId == userId))
                throw new Exception($"User with ID: {userId} already exists in this group.");
        }

        private void AssertThatRequestCameFromCreator(GroupEntity group, int userId)
        {
            SecurityAssertions.AssertThatIssuerIsAuthorizedToOperation(group, userId);
        }
    }
}