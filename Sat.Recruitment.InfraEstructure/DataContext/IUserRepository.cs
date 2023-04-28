using Sat.Recruitment.InfraEstructure.Models.Domain.Entities.Users;

namespace Sat.Recruitment.InfraEstructure.DataContext
{
    public interface IUserRepository
    {
        User Create(User user);
        List<User> GetAll();
        User? FindByEmail(string email);
        User? FindByPhone(string phone);
        User? FindBy(string name, string address);
    }
}
