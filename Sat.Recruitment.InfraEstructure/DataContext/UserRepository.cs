using Microsoft.Extensions.Logging;
using Sat.Recruitment.Common;
using Sat.Recruitment.InfraEstructure.Models.Domain.Entities.Users;

namespace Sat.Recruitment.InfraEstructure.DataContext
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private static readonly List<User> users = new();
        private readonly ILoadUsers _loadUsers;

        public UserRepository(ILoadUsers loadUsers, ILogger<UserRepository> logger)
        {
            _loadUsers = loadUsers;
            _logger = logger;
            InitUsers();
        }

        private void InitUsers()
        {
            try
            {
                var attributes = _loadUsers.FromFile(',', "Resources/users.txt");

                foreach (var attribute in attributes)
                {
                    var name = attribute[0] ?? string.Empty;
                    var email = attribute[1] ?? string.Empty;
                    var phone = attribute[2] ?? string.Empty;
                    var address = attribute[3] ?? string.Empty;

                    var strUserType = attribute[4] ?? "";
                    var userType = UserType.None;

                    if (!string.IsNullOrWhiteSpace(strUserType))
                    {
                        _ = Enum.TryParse(strUserType, out userType);
                    }

                    var strMoney = attribute[5] ?? "0";
                    decimal money = 0;
                    if (!string.IsNullOrWhiteSpace(strMoney))
                        money = Convert.ToDecimal(strMoney);

                    var user = new User(name, email, address, phone, money, userType);
                    users.Add(user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                if (ex.InnerException != null)
                    _logger.LogError(ex.InnerException.Message);
            }
        }

        public List<User> GetAll()
        {
            return users;
        }

        public User Create(User user)
        {
            users.Add(user);

            return user;
        }

        public User? FindBy(string name, string address)
        {
            var _user = users.FirstOrDefault(u => u.Name == name && u.Address == address);
            return _user;
        }

        public User? FindByEmail(string email)
        {
            var _user = users.FirstOrDefault(u => u.Email == email);
            return _user;
        }

        public User? FindByPhone(string phone)
        {
            var _user = users.FirstOrDefault(u => u.Phone == phone);
            return _user;
        }
    }
}
