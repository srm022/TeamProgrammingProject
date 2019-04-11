using System;
using AutoMapper;
using PZProject.Data.Repositories.Group;
using System.Collections.Generic;
using PZProject.Data.Requests.GroupRequests;

namespace PZProject.Handlers.Group
{
    public interface IGroupOperationsHandler
    {
        void CreateNewGroup(CreateGroupRequest request, int userId);
        List<Data.Database.Entities.Group.Group> GetGroupsForUser(int userId);
        void DeleteGroup(DeleteGroupRequest request, int userId);
    }

    public class GroupOperationsHandler: IGroupOperationsHandler
    {
        private readonly IGroupRepository _groupRepository;

        public GroupOperationsHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public void CreateNewGroup(CreateGroupRequest request, int userId)
        {
            VerifyNameAvailability(request.Name);
            var group = MapRequestToEntity(request);
            group.CreatorId = userId;
            Create(group);
        }

        public List<Data.Database.Entities.Group.Group> GetGroupsForUser(int userId)
        {
            return _groupRepository.GetGroupsForUser(userId);
        }

        public void DeleteGroup(DeleteGroupRequest request, int userId)
        {
            var groupId = request.GroupId;
            _groupRepository.DeleteGroup(groupId, userId);
        }

        private void VerifyNameAvailability(string name)
        {
            _groupRepository.VerifyIfGroupExistsForName(name);
        }

        private void Create(Data.Database.Entities.Group.Group group)
        {
            _groupRepository.CreateGroup(group);
        }

        private Data.Database.Entities.Group.Group MapRequestToEntity(CreateGroupRequest request)
        {
            return Mapper.Map<Data.Database.Entities.Group.Group>(request);
        }
    }
}
