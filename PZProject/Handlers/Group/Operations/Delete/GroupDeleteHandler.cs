using PZProject.Data.Database.Entities;
using PZProject.Data.Database.Entities.Group;
using PZProject.Data.Repositories.Group;
using PZProject.Handlers.Utils;

namespace PZProject.Handlers.Group.Operations.Delete
{
    public interface IGroupDeleteHandler
    {
        void DeleteGroup(int groupId, int issuerId);
    }

    public class GroupDeleteHandler : IGroupDeleteHandler
    {
        private readonly IGroupRepository _groupRepository;

        public GroupDeleteHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public void DeleteGroup(int groupId, int issuerId)
        {
            var group = GetGroupForId(groupId);
            AssertThatRequestCameFromCreator(group, issuerId);

            _groupRepository.DeleteGroup(group);
        }

        private GroupEntity GetGroupForId(int requestGroupId)
        {
            var group = _groupRepository.GetGroupById(requestGroupId);
            group.AssertThatExists();

            return group;
        }

        private void AssertThatRequestCameFromCreator(GroupEntity group, int userId)
        {
            SecurityAssertions.AssertThatIssuerIsAuthorizedToManageGroup(group, userId);
        }
    }
}