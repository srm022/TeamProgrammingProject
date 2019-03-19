using AutoMapper;
using PZProject.Data.Database.Entities;
using PZProject.Data.Database.Entities.User;
using PZProject.Handlers.User.Model;

namespace PZProject.Data
{
    public class SystemMapperProfile : Profile
    {
        public SystemMapperProfile()
        {
            CreateMap<RegisterUserModel, User>();
        }
    }
}