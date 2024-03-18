using AutoMapper;
using Domain.Entities;
using Shared.ViewModels.User;

namespace Application.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserView>().ReverseMap();
            CreateMap<User, NewUser>().ReverseMap();
            CreateMap<User, LoggedInUser>().ReverseMap();
        }
    }
}
