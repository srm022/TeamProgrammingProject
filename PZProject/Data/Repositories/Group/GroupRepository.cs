using Microsoft.EntityFrameworkCore;
using PZProject.Data.Database;
using PZProject.Data.Database.Entities.Group;
using System.Collections.Generic;
using System.Linq;

namespace PZProject.Data.Repositories.Group
{
    public interface IGroupRepository
    {
        GroupEntity GetGroupByName(string name);
        GroupEntity GetGroupById(int groupId);
        List<GroupEntity> GetGroupsForUser(int userId);

        GroupEntity CreateGroup(GroupEntity groupEntity);
        void AssignUserToGroup(int userId, int groupId);
        void DeleteGroup(GroupEntity group);
        void RemoveFromGroup(List<UserGroupEntity> group, int userId);
    }

    public class GroupRepository : IGroupRepository
    {
        private readonly SystemDbContext _db;

        public GroupRepository(SystemDbContext db)
        {
            _db = db;
        }

        public GroupEntity CreateGroup(GroupEntity groupEntity)
        {
            _db.Groups.Add(groupEntity);
            SaveChanges();

            return groupEntity;
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

        public void RemoveFromGroup(List<UserGroupEntity> group, int userId)
        {
            var userGroup = group.Single(ug => ug.UserId == userId);

            _db.UserGroups.Remove(userGroup);
            SaveChanges();
        }

        public GroupEntity GetGroupByName(string name)
        {
            return _db.Groups
                .Include(ug => ug.UserGroups)
                .SingleOrDefault(g => g.Name == name);
        }

        public List<GroupEntity> GetGroupsForUser(int userId)
        {
            var groupsIds = _db.UserGroups
                .Where(g => g.UserId == userId)
                .Select(g => g.GroupId)
                .ToList();

            var groups = _db.Groups
                .Where(x => groupsIds.Contains(x.GroupId))
                .Include(g => g.UserGroups)
                .ToList();

            return groups;
        }

        public void DeleteGroup(GroupEntity group)
        {
            _db.Groups.Remove(group);
            SaveChanges();
        }

        public GroupEntity GetGroupById(int groupId)
        {
            return _db.Groups
                .Include(ug => ug.UserGroups)
                .SingleOrDefault(g => g.GroupId == groupId);
        }

        private void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
