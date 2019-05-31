using PZProject.Data.Database.Entities;
using PZProject.Data.Database.Entities.Group;
using PZProject.Data.Database.Entities.User;
using PZProject.Data.Repositories.Group;
using PZProject.Data.Repositories.User;
using PZProject.Data.Requests.GroupRequests;
using PZProject.Data.Responses.GroupResponses;
using PZProject.Handlers.Group.Operations.AssignUser;
using PZProject.Handlers.Group.Operations.Create;
using PZProject.Handlers.Group.Operations.Delete;
using PZProject.Handlers.Group.Operations.RemoveUser;
using System.Collections.Generic;

namespace PZProject.Handlers.Group
{
    public interface IGroupOperationsHandler
    {
        void CreateNewGroup(CreateGroupRequest request, int issuerId);
        List<GroupResponse> GetGroupsForUser(int userId);
        void DeleteGroup(DeleteGroupRequest request, int issuerId);
        void AssignUserToGroup(AssignUserToGroupRequest request, int issuerId);
        void RemoveUserFromGroup(RemoveUserFromGroupRequest request, int issuerId);
    }

    public class GroupOperationsHandler : IGroupOperationsHandler
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGroupCreator _groupCreator;
        private readonly IGroupDeleteHandler _groupDeleteHandler;
        private readonly IGroupAssignUserHandler _groupAssignUserHandler;
        private readonly IGroupRemoveUserHandler _groupRemoveHandler;

        public GroupOperationsHandler(IGroupRepository groupRepository,
            IUserRepository userRepository,
            IGroupCreator groupCreator, 
            IGroupDeleteHandler groupDeleteHandler,
            IGroupAssignUserHandler groupAssignUserHandler,
            IGroupRemoveUserHandler groupRemoveHandler)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _groupCreator = groupCreator;
            _groupDeleteHandler = groupDeleteHandler;
            _groupAssignUserHandler = groupAssignUserHandler;
            _groupRemoveHandler = groupRemoveHandler;
        }

        public List<GroupResponse> GetGroupsForUser(int userId)
        {
            var groups = _groupRepository.GetGroupsForUser(userId);
            var groupResponses = new List<GroupResponse>();

            foreach (GroupEntity group in groups)
            {
                var groupResponse = new GroupResponse(group.GroupId, group.CreatorId, group.Name, group.Description, group.UserGroups);
                groupResponses.Add(groupResponse);
            }
            return groupResponses;
        }

        public void CreateNewGroup(CreateGroupRequest request, int issuerId)
        {
            var user = GetUserForId(issuerId);
            _groupCreator.CreateNewGroup(request, user.UserId);
            _groupAssignUserHandler.AssignUserToGroup(request.GroupName, user.UserId, issuerId);
        }

        public void AssignUserToGroup(AssignUserToGroupRequest request, int issuerId)
        {
            var user = GetUserForEmail(request.UserEmail);
            _groupAssignUserHandler.AssignUserToGroup(request.GroupName, user.UserId, issuerId);
        }

        public void RemoveUserFromGroup(RemoveUserFromGroupRequest request, int issuerId)
        {
            var user = GetUserForId(request.UserId);
            _groupRemoveHandler.RemoveUserFromGroup(request.GroupId, user.UserId, issuerId);
        }

        public void DeleteGroup(DeleteGroupRequest request, int issuerId)
        {
            _groupDeleteHandler.DeleteGroup(request.GroupId, issuerId);
        }

        private UserEntity GetUserForEmail(string userEmail)
        {
            var user = _userRepository.GetUserByEmail(userEmail);
            user.AssertThatExists();

            return user;
        }

        private UserEntity GetUserForId(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            user.AssertThatExists();

            return user;
        }
    }
}
