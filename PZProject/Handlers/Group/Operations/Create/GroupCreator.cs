using AutoMapper;
using PZProject.Data.Database.Entities.Group;
using PZProject.Data.Repositories.Group;
using PZProject.Data.Requests.GroupRequests;
using PZProject.Handlers.Group.Operations.Create.Model;
using System;

namespace PZProject.Handlers.Group.Operations.Create
{
    public interface IGroupCreator
    {
        GroupEntity CreateNewGroup(CreateGroupRequest request, int userId);
    }

    public class GroupCreator : IGroupCreator
    {
        private readonly IGroupRepository _groupRepository;

        public GroupCreator(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public GroupEntity CreateNewGroup(CreateGroupRequest request, int userId)
        {
            VerifyGroupNameAvailability(request.GroupName);

            var groupModel = CreateGroupModel(request, userId);
            var groupEntity = MapModelToEntity(groupModel);

            return Create(groupEntity);
        }

        private static CreateGroupModel CreateGroupModel(CreateGroupRequest request, int userId)
        {
            var groupModel = new CreateGroupModel
            {
                CreatorId = userId,
                GroupName = request.GroupName
            };

            return groupModel;
        }

        private GroupEntity Create(GroupEntity group)
        {
            return _groupRepository.CreateGroup(group);
        }

        private GroupEntity MapModelToEntity<T>(T model)
        {
            return Mapper.Map<GroupEntity>(model);
        }

        private void VerifyGroupNameAvailability(string name)
        {
            if (_groupRepository.GetGroupByName(name) != null)
                throw new Exception($"Group name {name} is already taken");
        }
    }
}