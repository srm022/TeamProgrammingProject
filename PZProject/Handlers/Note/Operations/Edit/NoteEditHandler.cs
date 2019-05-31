using System;
using PZProject.Data.Database.Entities;
using PZProject.Data.Database.Entities.Note;
using PZProject.Data.Repositories.Note;
using PZProject.Handlers.Utils;

namespace PZProject.Handlers.Note.Operations.Edit
{
    public interface INoteEditHandler
    {
        void EditNote(int noteId, string noteName, string noteDescription, int issuerId);
    }
    public class NoteEditHandler: INoteEditHandler
    {
        private readonly INoteRepository _noteRepository;

        public NoteEditHandler(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public void EditNote(int noteId, string noteName, string noteDescription, int issuerId)
        {
            var note = GetNoteForId(noteId);
            AssertThatRequestCameFromCreator(note, issuerId);

            _noteRepository.EditNote(note, noteName, noteDescription);
        }

        private NoteEntity GetNoteForId(int requestNoteId)
        {
            var note = _noteRepository.GetNoteById(requestNoteId);
            note.AssertThatExists();

            return note;
        }

        private void AssertThatRequestCameFromCreator(NoteEntity note, int userId)
        {
            SecurityAssertions.AssertThatIssuerIsAuthorizedToOperation(note, userId);
        }
    }
}
