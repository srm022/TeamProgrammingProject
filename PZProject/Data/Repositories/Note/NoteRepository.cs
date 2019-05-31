using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PZProject.Data.Database;
using PZProject.Data.Database.Entities.Note;

namespace PZProject.Data.Repositories.Note
{
    public interface INoteRepository
    {
        List<NoteEntity> GetNotesForGroup(int groupId, int userId);
        NoteEntity CreateNote(NoteEntity noteEntity, int groupId, int userId);
        void EditNote(NoteEntity note, string noteName, string noteDescription);
        NoteEntity GetNoteById(int noteId);
    }

    public class NoteRepository: INoteRepository
    {
        private readonly SystemDbContext _db;

        public NoteRepository(SystemDbContext db)
        {
            _db = db;
        }

        public List<NoteEntity> GetNotesForGroup(int groupId, int userId)
        {

            if (!IsGroupMember(groupId, userId))
            {
                throw new Exception("Cannot get notes for group You don't belong to!");
            }

            var notes = _db.Notes
                .Where(n => n.Group.GroupId == groupId)
                .Include(n => n.Group)
                .ToList();

            return notes;
        }

        public NoteEntity CreateNote(NoteEntity noteEntity, int groupId, int userId)
        {
            if (!IsGroupMember(groupId, userId))
            {
                throw new Exception("Cannot create notes for group You don't belong to!");
            }

            var group = _db.Groups
                .SingleOrDefault(g => g.GroupId == groupId);

            noteEntity.Group = group;

            _db.Notes.Add(noteEntity);
            SaveChanges();

            return noteEntity;
        }

        public void EditNote(NoteEntity note, string noteName, string noteDescription)
        {
            note.Name = noteName ?? note.Name;
            note.Description = noteDescription ?? note.Description;
            SaveChanges();
        }

        public NoteEntity GetNoteById(int noteId)
        {
            return _db.Notes
                .Include(g => g.Group)
                .SingleOrDefault(n => n.NoteId == noteId);
        }

        private bool IsGroupMember(int groupId, int userId)
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
