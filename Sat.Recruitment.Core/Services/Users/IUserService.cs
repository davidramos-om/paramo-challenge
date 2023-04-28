using Sat.Recruitment.InfraEstructure.Models.DTOs;

namespace Sat.Recruitment.Core.Services.Users
{
    public interface IUserService
    {
        UserDto? FindByEmail(string email);
        List<UserDto> GetAll();
        UserDto RegisterUser(RegisterUserDto userRegistration);
    }
}