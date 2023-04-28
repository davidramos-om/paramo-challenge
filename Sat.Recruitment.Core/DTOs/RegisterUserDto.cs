using System.ComponentModel.DataAnnotations;
using Sat.Recruitment.Common;

namespace Sat.Recruitment.Api.Core.DTOs
{
    public interface IRegisterUser
    {
        string Name { get; }
        string Email { get; }
        string Address { get; }
        string Phone { get; }
        decimal Money { get; }
        UserType UserType { get; }
    }

    public class RegisterUserDto : IRegisterUser
    {
        [Required(ErrorMessage = "The name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The email is required")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(300, ErrorMessage = "Address too long.")]
        public string Address { get; set; } = string.Empty;

        [MaxLength(20, ErrorMessage = "Phone too long.")]
        public string Phone { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "The amount must be a positive number")]
        public decimal Money { get; set; }

        [Required(ErrorMessage = "Must set a valid user type.")]
        public UserType UserType { get; set; }
    }
}
