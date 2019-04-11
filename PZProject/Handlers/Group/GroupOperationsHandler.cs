using AutoMapper;
using PZProject.Data.Database.Entities;
using PZProject.Data.Database.Entities.Group;
using PZProject.Data.Repositories.Group;
using PZProject.Data.Repositories.User;
using PZProject.Data.Requests.GroupRequests;
using PZProject.Handlers.Group.Model;
using System.Collections.Generic;

namespace PZProject.Handlers.Group
{
    public interface IGroupOperationsHandler
    {
        void CreateNewGroup(CreateGroupRequest request, int userId);
        List<GroupEntity> GetGroupsForUser(int userId);
        void DeleteGroup(DeleteGroupRequest request, int userId);
    }

    public class GroupOperationsHandler: IGroupOperationsHandler
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;

        public GroupOperationsHandler(IGroupRepository groupRepository,
            IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public void CreateNewGroup(CreateGroupRequest request, int userId)
        {
            VerifyNameAvailability(request.Name);
            VerifyIfUserExists(userId);

            var groupModel = CreateGroupModel(request, userId);
            var groupEntity = MapModelToEntity(groupModel);
            Create(groupEntity);
        }

        private void VerifyNameAvailability(string name)
        {
            _groupRepository.VerifyIfGroupExistsForName(name);
        }

        private void VerifyIfUserExists(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            user.AssertThatExists();
        }

        private void Create(GroupEntity group)
        {
            var groupId = _groupRepository.CreateGroup(group);
            _groupRepository.AssignUserToGroup(group.CreatorId, groupId);
        }

        public void DeleteGroup(DeleteGroupRequest request, int userId)
        {
            var groupId = request.GroupId;
            _groupRepository.DeleteGroup(groupId, userId);
        }

        public List<GroupEntity> GetGroupsForUser(int userId)
        {
            return _groupRepository.GetGroupsForUser(userId);
        }

        private GroupEntity MapModelToEntity<T>(T model)
        {
            return Mapper.Map<GroupEntity>(model);
        }

        private CreateGroupModel CreateGroupModel(CreateGroupRequest request, int userId)
        {
            return new CreateGroupModel
            {
                CreatorId = userId,
                Name = request.Name
            };
        }
    }
}
