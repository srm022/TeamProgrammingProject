using AutoMapper;
using PZProject.Data.Database.Entities.Group;
using PZProject.Data.Database.Entities.User;
using PZProject.Data.Requests.UserRequests;
using PZProject.Handlers.Group.Model;

namespace PZProject.Data
{
    public class SystemMapperProfile : Profile
    {
        public SystemMapperProfile()
        {
            CreateMap<RegisterUserRequest, UserEntity>();
            CreateMap<CreateGroupModel, GroupEntity>()
                .ForMember(group => group.CreatorId, opt => opt.MapFrom(model => model.CreatorId));
        }
    }
}