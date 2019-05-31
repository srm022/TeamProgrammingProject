using System;
using AutoMapper;
using PZProject.Data.Database.Entities.Note;
using PZProject.Data.Repositories.Note;
using PZProject.Data.Requests.NoteRequests;
using PZProject.Handlers.Group.Operations.CreateNote.Model;

namespace PZProject.Handlers.Group.Operations.CreateNote
{
    public interface INoteCreator
    {
        NoteEntity CreateNewNoteForGroup(CreateNoteRequest request, int groupId, int userId);
    }

    public class NoteCreator: INoteCreator
    {
        private readonly INoteRepository _noteRepository;

        public NoteCreator(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public NoteEntity CreateNewNoteForGroup(CreateNoteRequest request, int groupId, int userId)
        {
            var noteModel = CreateNoteModel(request, userId);
            var noteEntity = MapModelToEntity(noteModel);

            return Create(noteEntity, groupId, userId);
        }

        private CreateNoteModel CreateNoteModel(CreateNoteRequest request, int userId)
        {
            var noteModel = new CreateNoteModel
            {
                CreatorId = userId,
                NoteName = request.NoteName,
                NoteDescription = request.NoteDescription
            };

            return noteModel;
        }

        private NoteEntity Create(NoteEntity note, int groupId, int userId)
        {
            return _noteRepository.CreateNote(note, groupId, userId);
        }

        private NoteEntity MapModelToEntity<T>(T model)
        {
            return Mapper.Map<NoteEntity>(model);
        }
    }
}
