using AutoMapper;
using Entities.Models.DTOs;
using Entities;
using Entities.Models.DTOs.User;

namespace MapperHelper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegistrationDto, User>();
        }
    }
}
