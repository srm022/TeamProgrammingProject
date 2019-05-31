using AutoMapper;
using PZProject.Data.Database.Entities.Group;
using PZProject.Data.Database.Entities.Note;
using PZProject.Data.Database.Entities.User;
using PZProject.Data.Requests.UserRequests;
using PZProject.Handlers.Group.Operations.Create.Model;
using PZProject.Handlers.Group.Operations.CreateNote.Model;

namespace PZProject.Data
{
    public class SystemMapperProfile : Profile
    {
        public SystemMapperProfile()
        {
            CreateMap<RegisterUserRequest, UserEntity>();
            CreateMap<CreateGroupModel, GroupEntity>()
                .ForMember(group => group.CreatorId, opt => opt.MapFrom(model => model.CreatorId))
                .ForMember(group => group.Name, opt => opt.MapFrom(model => model.GroupName));
            CreateMap<CreateNoteModel, NoteEntity>()
                .ForMember(note => note.CreatorId, opt => opt.MapFrom(model => model.CreatorId))
                .ForMember(note => note.Name, opt => opt.MapFrom(model => model.NoteName))
                .ForMember(note => note.Description, opt => opt.MapFrom(model => model.NoteDescription));
        }
    }
}