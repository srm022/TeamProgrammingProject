using System;
using System.Collections.Generic;
using System.Linq;
using PZProject.Data.Database;
using PZProject.Data.Database.Entities.GroupNote;

namespace PZProject.Data.Repositories.GroupNote
{
    public interface IGroupNoteRepository
    {
        List<GroupNoteEntity> GetGroupNotesForGroup(int groupId, int userId);
        GroupNoteEntity CreateNote(GroupNoteEntity noteEntity, int groupId, int userId);
    }

    public class GroupNoteRepository: IGroupNoteRepository
    {
        private readonly SystemDbContext _db;

        public GroupNoteRepository(SystemDbContext db)
        {
            _db = db;
        }

        public List<GroupNoteEntity> GetGroupNotesForGroup(int groupId, int userId)
        {

            if (!isGroupMember(groupId, userId))
            {
                throw new Exception("Cannot get notes for group You don't belong to!");
            }

            var notes = _db.GroupNotes
                .Where(n => n.Group.GroupId == groupId)
                .ToList();

            return notes;
        }

        public GroupNoteEntity CreateNote(GroupNoteEntity noteEntity, int groupId, int userId)
        {
            if (!isGroupMember(groupId, userId))
            {
                throw new Exception("Cannot create notes for group You don't belong to!");
            }

            var group = _db.Groups
                .SingleOrDefault(g => g.GroupId == groupId);

            noteEntity.Group = group;

            _db.GroupNotes.Add(noteEntity);
            SaveChanges();

            return noteEntity;
        }

        private bool isGroupMember(int groupId, int userId)
        {
            return _db.UserGroups
                .Any(g => g.GroupId == groupId && g.UserId == userId);
        }

        private void SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
