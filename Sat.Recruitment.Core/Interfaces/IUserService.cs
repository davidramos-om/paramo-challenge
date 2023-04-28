using Sat.Recruitment.Api.Core.DTOs;

namespace Sat.Recruitment.Core.Interfaces
{
    public interface IUserService
    {
        Task<UserDto?> FindByEmail(string email);
        Task<List<UserDto>> GetAll();
        Task<UserDto> RegisterUser(RegisterUserDto userRegistration);
    }
}