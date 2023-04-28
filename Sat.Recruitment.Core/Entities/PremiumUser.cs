using Sat.Recruitment.Common;
using Sat.Recruitment.Core.Abstract;
using Sat.Recruitment.Core.Interfaces;

namespace Sat.Recruitment.Core.Entities
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
