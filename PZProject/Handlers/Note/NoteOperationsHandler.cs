using PZProject.Data.Database.Entities.Note;
using PZProject.Data.Repositories.Note;
using PZProject.Data.Requests.NoteRequests;
using PZProject.Data.Responses.NotesResponses;
using PZProject.Handlers.Group.Operations.CreateNote;
using PZProject.Handlers.Note.Operations.Delete;
using PZProject.Handlers.Note.Operations.Edit;
using System.Collections.Generic;

namespace PZProject.Handlers.Note
{
    public interface INoteOperationsHandler
    {
        List<NoteResponse> GetNotesForGroup(int groupId, int issuerId);
        void CreateNoteForGroup(CreateNoteRequest request, int groupId, int issuerId);
        void EditNote(EditNoteRequest request, int issuerId);
        void DeleteNote(DeleteNoteRequest request, int issuerId);
    }

    public class NoteOperationsHandler : INoteOperationsHandler
    {
        private readonly INoteRepository _notesRepository;
        private readonly INoteCreator _notesCreator;
        private readonly INoteEditHandler _noteEditHandler;
        private readonly INoteDeleteHandler _noteDeleteHandler;

        public NoteOperationsHandler(INoteRepository noteRepository,
            INoteCreator notesCreator,
            INoteEditHandler noteEditHandler,
            INoteDeleteHandler noteDeleteHandler)
        {
            _notesRepository = noteRepository;
            _notesCreator = notesCreator;
            _noteEditHandler = noteEditHandler;
            _noteDeleteHandler = noteDeleteHandler;
        }

        public List<NoteResponse> GetNotesForGroup(int groupId, int issuerId)
        {
            var notes = _notesRepository.GetNotesForGroup(groupId, issuerId);
            var noteResponses = new List<NoteResponse>();

            foreach (NoteEntity note in notes)
            {
                var noteResponse = new NoteResponse(note);
                noteResponses.Add(noteResponse);
            }

            return noteResponses;
        }

        public void CreateNoteForGroup(CreateNoteRequest request, int groupId, int issuerId)
        {
            _notesCreator.CreateNewNoteForGroup(request, groupId, issuerId);
        }

        public void EditNote(EditNoteRequest request, int issuerId)
        {
            _noteEditHandler.EditNote(request.NoteId, request.NoteName, request.NoteDescription, issuerId);
        }

        public void DeleteNote(DeleteNoteRequest request, int issuerId)
        {
            _noteDeleteHandler.DeleteNote(request.NoteId, issuerId);
        }
    }
}