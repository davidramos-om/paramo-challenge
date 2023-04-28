using Sat.Recruitment.Common;
using Sat.Recruitment.InfraEstructure.Models.Domain.Interfaces;

namespace Sat.Recruitment.InfraEstructure.Models.Domain.Entities.Users
{
    public sealed class PremiumUser : User, IUserGif
    {
        public PremiumUser(string name, string email, string address, string phone, decimal money, UserType userType) : base(name, email, address, phone, money, userType)
        {
            CalculateGif();
        }

        public void CalculateGif()
        {
            if (Money > 100)
            {
                decimal gif = Money * 2;
                Money += gif;
            }
        }
    }
}
