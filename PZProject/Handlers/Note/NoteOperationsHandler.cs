using PZProject.Data.Database.Entities;
using PZProject.Data.Database.Entities.Group;
using PZProject.Data.Database.Entities.Note;
using PZProject.Data.Database.Entities.User;
using PZProject.Data.Repositories.Group;
using PZProject.Data.Repositories.Note;
using PZProject.Data.Repositories.User;
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
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;

        public NoteOperationsHandler(INoteRepository noteRepository,
            INoteCreator notesCreator,
            INoteEditHandler noteEditHandler,
            INoteDeleteHandler noteDeleteHandler,
            IUserRepository userRepository,
            IGroupRepository groupRepository)
        {
            _notesRepository = noteRepository;
            _notesCreator = notesCreator;
            _noteEditHandler = noteEditHandler;
            _noteDeleteHandler = noteDeleteHandler;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
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
            var user = GetUserForId(issuerId);
            var group = GetGroupForId(groupId);
            _notesCreator.CreateNewNoteForGroup(request, group.GroupId, user.UserId);
        }

        public void EditNote(EditNoteRequest request, int issuerId)
        {
            var user = GetUserForId(issuerId);
            var note = GetNoteForId(request.NoteId);
            _noteEditHandler.EditNote(note, request.NoteName, request.NoteDescription, user.UserId);
        }

        public void DeleteNote(DeleteNoteRequest request, int issuerId)
        {
            var user = GetUserForId(issuerId);
            _noteDeleteHandler.DeleteNote(request.NoteId, user.UserId);
        }

        private UserEntity GetUserForId(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            user.AssertThatExists();

            return user;
        }

        private GroupEntity GetGroupForId(int groupId)
        {
            var group = _groupRepository.GetGroupById(groupId);
            group.AssertThatExists();

            return group;
        }

        private NoteEntity GetNoteForId(int noteId)
        {
            var note = _notesRepository.GetNoteById(noteId);
            note.AssertThatExists();

            return note;
        }
    }
}