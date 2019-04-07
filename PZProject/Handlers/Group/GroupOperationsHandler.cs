using System;
using AutoMapper;
using PZProject.Handlers.Group.Model;
using PZProject.Data.Repositories.Group;
namespace PZProject.Handlers.Group
{
    public interface IGroupOperationsHandler
    {
        void CreateNewGroup(CreateGroupModel model);
    }

    public class GroupOperationsHandler
    {
        private readonly IGroupRepository _groupRepository;

        public GroupOperationsHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public void CreateNewGroup(CreateGroupModel model)
        {
            VerifyNameAvailability(model.Name);
            var group = MapModelToEntity(model);
            Create(group);
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
