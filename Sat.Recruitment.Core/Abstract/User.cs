using Sat.Recruitment.Common;
using Sat.Recruitment.Core.Interfaces;

namespace Sat.Recruitment.Core.Abstract
{
    public class User : IUser
    {
        public Guid Id { get; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public decimal Money { get; set; } = 0;
        public UserType UserType { get; set; } = UserType.None;

        public User(string name, string email, string address, string phone, decimal money, UserType userType)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            Money = money;
            UserType = userType;
        }         
    }
}
