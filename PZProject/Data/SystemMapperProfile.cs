using AutoMapper;
using PZProject.Data.Database.Entities.Group;
using PZProject.Data.Database.Entities.User;
using PZProject.Data.Requests.UserRequests;
using PZProject.Handlers.Group.Operations.Create.Model;

namespace PZProject.Data
{
    public class SystemMapperProfile : Profile
    {
        public SystemMapperProfile()
        {
            CreateMap<RegisterUserRequest, UserEntity>();
            CreateMap<CreateGroupModel, GroupEntity>()
                .ForMember(group => group.CreatorId, opt => opt.MapFrom(model => model.CreatorId))
                .ForMember(group => group.Name, opt => opt.MapFrom(model => model.GroupName))
                .ForMember(group => group.Description, opt => opt.MapFrom(model => model.GroupDescription));
        }
    }
}