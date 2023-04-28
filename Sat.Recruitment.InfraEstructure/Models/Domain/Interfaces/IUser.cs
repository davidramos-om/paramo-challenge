using Sat.Recruitment.Common;

namespace Sat.Recruitment.InfraEstructure.Models.Domain.Interfaces
{
    public interface IUser
    {
        Guid Id { get; }
        string Name { get; set; }
        string Email { get; set; }
        string Address { get; set; }
        string Phone { get; set; }
        decimal Money { get; set; }
        UserType UserType { get; set; }
    }
}
