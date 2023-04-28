using Sat.Recruitment.Common;
using Sat.Recruitment.Core.Abstract;
using Sat.Recruitment.Core.Interfaces;

namespace Sat.Recruitment.Core.Entities
{
    public sealed class SuperUser : User, IUserGif
    {
        public SuperUser(string name, string email, string address, string phone, decimal money, UserType userType) : base(name, email, address, phone, money, userType)
        {
            CalculateGif();
        }

        public void CalculateGif()
        {
            if (Money > 100)
            {
                decimal percentage = Convert.ToDecimal(0.20);
                decimal gif = Money * percentage;
                Money += gif;
            }
        }
    }
}
