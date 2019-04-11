using AutoMapper;
using PZProject.Data.Database.Entities;
using PZProject.Data.Database.Entities.User;
using PZProject.Data.Database.Entities.Group;
using PZProject.Data.Requests.GroupRequests;
using PZProject.Data.Requests.UserRequests;

namespace PZProject.Data
{
    public class SystemMapperProfile : Profile
    {
        public SystemMapperProfile()
        {
            CreateMap<RegisterUserRequest, User>();
            CreateMap<CreateGroupRequest, Group>();
        }
    }
}