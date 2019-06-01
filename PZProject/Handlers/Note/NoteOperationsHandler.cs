using System;
using System.Collections.Generic;
using PZProject.Data.Database.Entities;
using PZProject.Data.Database.Entities.Note;
using PZProject.Data.Database.Entities.User;
using PZProject.Data.Repositories.Note;
using PZProject.Data.Repositories.User;
using PZProject.Data.Requests.NoteRequests;
using PZProject.Data.Responses.NotesResponses;
using PZProject.Handlers.Group.Operations.CreateNote;
using PZProject.Handlers.Note.Operations.Delete;
using PZProject.Handlers.Note.Operations.Edit;

namespace PZProject.Handlers.Note
{
    public interface INoteOperationsHandler
    {
        List<NoteResponse> GetNotesForGroup(int groupId, int issuerId);
        void CreateNoteForGroup(CreateNoteRequest request, int groupId, int issuerId);
        void EditNote(EditNoteRequest request, int issuerId);
        void DeleteNote(DeleteNoteRequest request, int issuerId);
    }

    public class NoteOperationsHandler: INoteOperationsHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly INoteRepository _notesRepository;
        private readonly INoteCreator _notesCreator;
        private readonly INoteEditHandler _noteEditHandler;
        private readonly INoteDeleteHandler _noteDeleteHandler;

        public int NoteResponse { get; private set; }

        public NoteOperationsHandler(IUserRepository userRepository, 
            INoteRepository noteRepository,
            INoteCreator notesCreator,
            INoteEditHandler noteEditHandler,
            INoteDeleteHandler noteDeleteHandler)
        {
            _userRepository = userRepository;
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
                var noteResponse = new NoteResponse(note.NoteId, note.CreatorId, note.Group.GroupId, note.Name, note.Description);
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

        private UserEntity GetUserForId(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            user.AssertThatExists();

            return user;
        }
    }
}
