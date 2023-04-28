using Sat.Recruitment.Core.Abstract;

namespace Sat.Recruitment.InfraEstructure.DataContext
{
    public interface IUserRepository
    {
        Task<User> Create(User user);
        Task<List<User>> GetAll();
        Task<User?> FindByEmail(string email);
        Task<User?> FindByPhone(string phone);
        Task<User?> FindBy(string name, string address);
    }
}
