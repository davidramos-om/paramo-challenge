using Sat.Recruitment.Common;
using Sat.Recruitment.InfraEstructure.Models.Domain.Interfaces;

namespace Sat.Recruitment.InfraEstructure.Models.Domain.Entities.Users
{
    public sealed class NormalUser : User, IUserGif
    {
        public NormalUser(string name, string email, string address, string phone, decimal money, UserType userType) : base(name, email, address, phone, money, userType)
        {
            CalculateGif();
        }

        public void CalculateGif()
        {
            // If user has more than USD100
            if (Money > 100)
            {
                decimal percentage = Convert.ToDecimal(0.12);
                decimal gif = Money * percentage;
                Money += gif;
            }

            // If has has more than 10 and less than 100 
            if (Money > 10 && Money < 100)
            {
                decimal percentage = Convert.ToDecimal(0.8);
                decimal gif = Money * percentage;
                Money += gif;
            }
        }
    }
}
