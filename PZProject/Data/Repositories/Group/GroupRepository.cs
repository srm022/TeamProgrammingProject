using PZProject.Data.Database;
using PZProject.Data.Database.Entities.Group;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PZProject.Data.Repositories.Group
{
    public interface IGroupRepository
    {
        int CreateGroup(GroupEntity groupEntity);
        void AssignUserToGroup(int userId, int groupId);
        void VerifyIfGroupExistsForName(string name);
        List<GroupEntity> GetGroupsForUser(int userId);
        void DeleteGroup(int groupId, int userId);
    }

    public class GroupRepository : IGroupRepository
    {
        private readonly SystemDbContext _db;

        public GroupRepository(SystemDbContext db)
        {
            _db = db;
        }

        public int CreateGroup(GroupEntity groupEntity)
        {
            _db.Groups.Add(groupEntity);
            SaveChanges();

            return groupEntity.GroupId;
        }

        public void AssignUserToGroup(int userId, int groupId)
        {
            var userGroup = new UserGroupEntity
            {
                GroupId = groupId,
                UserId = userId
            };

            _db.UserGroups.Add(userGroup);
            SaveChanges();
        }

        public void VerifyIfGroupExistsForName(string name)
        {
            if (_db.Groups.Any(x => x.Name == name))
                throw new Exception($"Name {name} is already taken");
        }

        public List<GroupEntity> GetGroupsForUser(int userId)
        {
            var groupsIds = _db.UserGroups
                .Where(g => g.UserId == userId)
                .Select(g => g.GroupId);

            var groups = _db.Groups.Where(x => groupsIds.Contains(x.GroupId)).ToList();

            return groups;
        }

        public void DeleteGroup(int groupId, int userId)
        {
            var group = _db.Groups.FirstOrDefault(g => g.GroupId == groupId);

            if (group.CreatorId != userId)
                throw new Exception("Only group creator can remove group!");

            _db.Groups.Remove(group);
            SaveChanges();
        }

        private void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
