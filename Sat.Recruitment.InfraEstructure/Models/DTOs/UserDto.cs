using Sat.Recruitment.Common;

namespace Sat.Recruitment.InfraEstructure.Models.DTOs
{
    public sealed class UserDto : IRegisterUser
    {
        public string UserId { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public decimal Money { get; set; }

        public UserType UserType { get; set; }
    }
}
