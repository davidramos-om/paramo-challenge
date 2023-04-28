using AutoMapper;
using Sat.Recruitment.InfraEstructure.Models.Domain.Entities.Users;
using Sat.Recruitment.InfraEstructure.Models.DTOs;

namespace Sat.Recruitment.Core.Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, RegisterUserDto>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<User, UserDto>().IgnoreAllPropertiesWithAnInaccessibleSetter();

            CreateMap<RegisterUserDto, User>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<RegisterUserDto, UserDto>().IgnoreAllPropertiesWithAnInaccessibleSetter();
      
            CreateMap<UserDto, User>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<UserDto, RegisterUserDto>().IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }
}
