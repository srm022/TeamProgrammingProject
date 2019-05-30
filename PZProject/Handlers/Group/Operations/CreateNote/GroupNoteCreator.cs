using System;
using AutoMapper;
using PZProject.Data.Database.Entities.GroupNote;
using PZProject.Data.Repositories.GroupNote;
using PZProject.Data.Requests.GroupRequests;
using PZProject.Handlers.Group.Operations.CreateNote.Model;

namespace PZProject.Handlers.Group.Operations.CreateNote
{
    public interface IGroupNoteCreator
    {
        GroupNoteEntity CreateNewNoteForGroup(CreateGroupNoteRequest request, int groupId, int userId);
    }

    public class GroupNoteCreator: IGroupNoteCreator
    {
        private readonly IGroupNoteRepository _groupNoteRepository;

        public GroupNoteCreator(IGroupNoteRepository groupNoteRepository)
        {
            _groupNoteRepository = groupNoteRepository;
        }

        public GroupNoteEntity CreateNewNoteForGroup(CreateGroupNoteRequest request, int groupId, int userId)
        {
            var noteModel = CreateNoteModel(request, userId);
            var noteEntity = MapModelToEntity(noteModel);

            return Create(noteEntity, groupId, userId);
        }

        private CreateNoteModel CreateNoteModel(CreateGroupNoteRequest request, int userId)
        {
            var noteModel = new CreateNoteModel
            {
                CreatorId = userId,
                NoteName = request.NoteName,
                NoteDescription = request.NoteDescription
            };

            return noteModel;
        }

        private GroupNoteEntity Create(GroupNoteEntity note, int groupId, int userId)
        {
            return _groupNoteRepository.CreateNote(note, groupId, userId);
        }

        private GroupNoteEntity MapModelToEntity<T>(T model)
        {
            return Mapper.Map<GroupNoteEntity>(model);
        }
    }
}
