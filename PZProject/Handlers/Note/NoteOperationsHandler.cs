using System;
using System.Collections.Generic;
using PZProject.Data.Database.Entities;
using PZProject.Data.Database.Entities.Note;
using PZProject.Data.Database.Entities.User;
using PZProject.Data.Repositories.Note;
using PZProject.Data.Repositories.User;
using PZProject.Data.Requests.NoteRequests;
using PZProject.Handlers.Group.Operations.CreateNote;

namespace PZProject.Handlers.Note
{
    public interface INoteOperationsHandler
    {
        List<NoteEntity> GetNotesForGroup(int groupId, int issuerId);
        void CreateNoteForGroup(CreateNoteRequest request, int groupId, int issuerId);
    }

    public class NoteOperationsHandler: INoteOperationsHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly INoteRepository _notesRepository;
        private readonly INoteCreator _notesCreator;

        public NoteOperationsHandler(IUserRepository userRepository, 
            INoteRepository noteRepository,
            INoteCreator notesCreator)
        {
            _userRepository = userRepository;
            _notesRepository = noteRepository;
            _notesCreator = notesCreator;
        }

        public List<NoteEntity> GetNotesForGroup(int groupId, int issuerId)
        {
            return _notesRepository.GetNotesForGroup(groupId, issuerId);
        }

        public void CreateNoteForGroup(CreateNoteRequest request, int groupId, int issuerId)
        {
            var user = GetUserForId(issuerId);
            _notesCreator.CreateNewNoteForGroup(request, groupId, issuerId);
        }

        private UserEntity GetUserForId(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            user.AssertThatExists();

            return user;
        }
    }
}
