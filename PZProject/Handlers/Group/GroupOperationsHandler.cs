using System;
using AutoMapper;
using PZProject.Handlers.Group.Model;
using PZProject.Data.Repositories.Group;
using System.Collections.Generic;

namespace PZProject.Handlers.Group
{
    public interface IGroupOperationsHandler
    {
        void CreateNewGroup(CreateGroupModel model, int userId);
        List<Data.Database.Entities.Group.Group> GetGroupsForUser(int userId);
        void DeleteGroup(int groupId, int userId);
    }

    public class GroupOperationsHandler: IGroupOperationsHandler
    {
        private readonly IGroupRepository _groupRepository;

        public GroupOperationsHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public void CreateNewGroup(CreateGroupModel model, int userId)
        {
            Console.Write("Creating");
            VerifyNameAvailability(model.Name);
            var group = MapModelToEntity(model);
            group.CreatorId = userId;
            Create(group);
        }

        public List<Data.Database.Entities.Group.Group> GetGroupsForUser(int userId)
        {
            return _groupRepository.GetGroupsForUser(userId);
        }

        public void DeleteGroup(int groupId, int userId)
        {
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

        private Data.Database.Entities.Group.Group MapModelToEntity(CreateGroupModel model)
        {
            return Mapper.Map<Data.Database.Entities.Group.Group>(model);
        }
    }
}
