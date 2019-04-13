using PZProject.Data.Database.Entities;
using PZProject.Data.Database.Entities.Group;
using PZProject.Data.Repositories.Group;
using PZProject.Handlers.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PZProject.Handlers.Group.Operations.RemoveUser
{
    public interface IGroupRemoveUserHandler
    {
        void RemoveUserFromGroup(int groupId, int userId, int issuerId);
    }

    public class GroupRemoveUserHandler : IGroupRemoveUserHandler
    {
        private readonly IGroupRepository _groupRepository;

        public GroupRemoveUserHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public void RemoveUserFromGroup(int groupId, int userId, int issuerId)
        {
            var group = GetGroupForId(groupId);
            AssertThatRequestCameFromCreator(group, issuerId);
            AssertThatUserIsAssignedToGroup(group.UserGroups, userId);

            RemoveFromGroup(group, userId);
        }

        private void RemoveFromGroup(GroupEntity group, int userId)
        {
            _groupRepository.RemoveFromGroup(group.UserGroups, userId);
        }

        private GroupEntity GetGroupForId(int requestGroupId)
        {
            var group = _groupRepository.GetGroupById(requestGroupId);
            group.AssertThatExists();

            return group;
        }

        private void AssertThatUserIsAssignedToGroup(List<UserGroupEntity> userGroups, int userId)
        {
            if (userGroups.All(ug => ug.UserId != userId))
                throw new Exception($"User with ID: {userId} already does not belong to this group.");
        }

        private void AssertThatRequestCameFromCreator(GroupEntity group, int userId)
        {
            SecurityAssertions.AssertThatIssuerIsAuthorizedToOperation(group, userId);
        }
    }
}