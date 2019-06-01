using System;
using PZProject.Data.Database.Entities;
using PZProject.Data.Database.Entities.Group;
using PZProject.Data.Database.Entities.Note;
using PZProject.Data.Repositories.Group;
using PZProject.Data.Repositories.Note;
using PZProject.Handlers.Utils;

namespace PZProject.Handlers.Note.Operations.Delete
{
    public interface INoteDeleteHandler
    {
        void DeleteNote(int noteId, int issuerId);
    }

    public class NoteDeleteHandler : INoteDeleteHandler
    {
        private readonly INoteRepository _noteRepository;
        private readonly IGroupRepository _groupRepository;

        public NoteDeleteHandler(INoteRepository noteRepository,
            IGroupRepository groupRepository)
        {
            _noteRepository = noteRepository;
            _groupRepository = groupRepository;
        }

        public void DeleteNote(int noteId, int issuerId)
        {
            var note = GetNoteForId(noteId);
            var group = GetGroupForNoteGroupId(note.Group.GroupId);
            AssertThatRequestCameFromCreatorOrGroupAdmin(note, group, issuerId);

            _noteRepository.DeleteNote(note);
        }

        private NoteEntity GetNoteForId(int requestNoteId)
        {
            var note = _noteRepository.GetNoteById(requestNoteId);
            note.AssertThatExists();

            return note;
        }

        private GroupEntity GetGroupForNoteGroupId(int groupId)
        {
            var group = _groupRepository.GetGroupById(groupId);
            group.AssertThatExists();

            return group;
        }

        private void AssertThatRequestCameFromCreatorOrGroupAdmin(NoteEntity note, GroupEntity group, int userId)
        {
            SecurityAssertions.AssertThatIssuerIsAuthorizedToDeleteNote(note, group, userId);
        }
    }
}
