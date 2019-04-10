using System;
using System.Collections.Generic;
using System.Linq;
using PZProject.Data.Database;

namespace PZProject.Data.Repositories.Group
{
    public interface IGroupRepository
    {
        void CreateGroup(Database.Entities.Group.Group groupEntity);
        void VerifyIfGroupExistsForName(string name);
        List<Database.Entities.Group.Group> GetGroupsForUser(int userId);
        void DeleteGroup(int groupId, int userId);
    }

    public class GroupRepository : IGroupRepository
    {
        private readonly SystemDbContext _db;

        public GroupRepository(SystemDbContext db)
        {
            _db = db;
        }

        public void CreateGroup(Database.Entities.Group.Group groupEntity)
        {
            _db.Groups.Add(groupEntity);
            SaveChanges();
            var user = _db.Users
                .Where(u => u.UserId == groupEntity.CreatorId)
                .FirstOrDefault();
            var userGroup = new Database.Entities.Group.UserGroup
            {
                GroupId = groupEntity.GroupId,
                UserId = user.UserId
            };
            _db.UserGroups.Add(userGroup);
            SaveChanges();
        }

        public void VerifyIfGroupExistsForName(string name)
        {
            if (_db.Groups.Any(x => x.Name == name))
                throw new Exception($"Name {name} is already taken");
        }

        public List<Database.Entities.Group.Group> GetGroupsForUser(int userId)
        {
            var groupsIds = _db.UserGroups
                .Where(g => g.UserId == userId)
                .Select(g => g.GroupId);

            var groups = _db.Groups.Where(x => groupsIds.Contains(x.GroupId)).ToList();

            return groups;
        }

        public void DeleteGroup(int groupId, int userId)
        {
            var group = _db.Groups
                .Where(g => g.GroupId == groupId)
                .FirstOrDefault();

            if (group.CreatorId != userId)
            {
                throw new Exception("Only group creator can remove group!");
            }
            _db.Groups.Remove(group);
            SaveChanges();
        }

        private void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
