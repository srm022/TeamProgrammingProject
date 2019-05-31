using System;
using PZProject.Data.Database.Entities;
using PZProject.Data.Database.Entities.Group;
using PZProject.Data.Repositories.Group;
using PZProject.Handlers.Utils;

namespace PZProject.Handlers.Group.Operations.Edit
{
    public interface IGroupEditHandler
    {
        void EditGroup(int groupId, string groupName, string groupDescription, int issuerId);
    }

    public class GroupEditHandler: IGroupEditHandler
    {
        private readonly IGroupRepository _groupRepository;

        public GroupEditHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public void EditGroup(int groupId, string groupName, string groupDescription, int issuerId)
        {
            var group = GetGroupForId(groupId);
            AssertThatRequestCameFromCreator(group, issuerId);

            _groupRepository.EditGroup(group, groupName, groupDescription);
        }

        private GroupEntity GetGroupForId(int requestGroupId)
        {
            var group = _groupRepository.GetGroupById(requestGroupId);
            group.AssertThatExists();

            return group;
        }

        private void AssertThatRequestCameFromCreator(GroupEntity group, int userId)
        {
            SecurityAssertions.AssertThatIssuerIsAuthorizedToOperation(group, userId);
        }
    }
}
