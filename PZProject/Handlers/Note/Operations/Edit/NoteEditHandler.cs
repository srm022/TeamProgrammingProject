using System;
using PZProject.Data.Database.Entities;
using PZProject.Data.Database.Entities.Note;
using PZProject.Data.Repositories.Note;
using PZProject.Handlers.Utils;

namespace PZProject.Handlers.Note.Operations.Edit
{
    public interface INoteEditHandler
    {
        void EditNote(NoteEntity note, string noteName, string noteDescription, int issuerId);
    }
    public class NoteEditHandler: INoteEditHandler
    {
        private readonly INoteRepository _noteRepository;

        public NoteEditHandler(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public void EditNote(NoteEntity note, string noteName, string noteDescription, int issuerId)
        {
            AssertThatRequestCameFromCreator(note, issuerId);
            _noteRepository.EditNote(note, noteName, noteDescription);
        }

        private void AssertThatRequestCameFromCreator(NoteEntity note, int userId)
        {
            SecurityAssertions.AssertThatIssuerIsAuthorizedToManageNote(note, userId);
        }
    }
}
