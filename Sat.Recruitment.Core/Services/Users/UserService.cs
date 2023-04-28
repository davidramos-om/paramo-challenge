using System.Text;
using AutoMapper;
using Sat.Recruitment.Common;
using Sat.Recruitment.InfraEstructure.DataContext;
using Sat.Recruitment.InfraEstructure.Models.Domain.Entities.Users;
using Sat.Recruitment.InfraEstructure.Models.DTOs;

namespace Sat.Recruitment.Core.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public List<UserDto> GetAll()
        {
            var users = _userRepository.GetAll();
            List<UserDto> dtos = new();

            foreach (var user in users)
            {
                var dto = _mapper.Map<UserDto>(user);
                dtos.Add(dto);
            }

            return dtos;
        }

        public UserDto? FindByEmail(string email)
        {
            var user = _userRepository.FindByEmail(email);
            if (user == null)
                return null;

            var transformed = _mapper.Map<UserDto>(user);
            return transformed;
        }

        public UserDto RegisterUser(RegisterUserDto userRegistration)
        {
            // Get required fields defined in the DTO (due to DataAnnotation valid only for HTTP Requests)
            Dictionary<string, string> requiredFields = ClassValidator.GetRequiredFields<RegisterUserDto>();

            if (requiredFields != null)
            {
                // Check if all the required fields were filled in
                ClassValidator.PopulateValueRequiredFields(userRegistration, requiredFields);
                List<string> errors = ClassValidator.ValidateRequiredFields(requiredFields);

                if (errors != null && errors.Count > 0)
                {
                    //generate a single string from list of errors
                    var msgError = new StringBuilder(errors.Count);
                    foreach (var error in errors)
                    {
                        msgError.AppendLine(error.ToString());
                    }

                    throw new Exception(msgError.ToString());
                }
            }

            if (!userRegistration.Email.IsValidEmail())
                throw new AppExceptionHandler("Invalid email");

            userRegistration.Email = userRegistration.Email.NormalizeEmail();

            var existing = _userRepository.FindByEmail(userRegistration.Email);
            if (existing != null)
                throw new AppExceptionHandler("Email already registered.");

            existing = _userRepository.FindByPhone(userRegistration.Phone);
            if (existing != null)
                throw new AppExceptionHandler("Phone number already used by someone else.");

            existing = _userRepository.FindBy(userRegistration.Name, userRegistration.Address);
            if (existing != null)
                throw new AppExceptionHandler("Already exists a user using the name and address entered.");

            User? newUser = null;

            switch (userRegistration.UserType)
            {
                case UserType.Normal:
                    newUser = new NormalUser(userRegistration.Name, userRegistration.Email, userRegistration.Address, userRegistration.Phone, userRegistration.Money, userRegistration.UserType);
                    break;
                case UserType.Superuser:
                    newUser = new SuperUser(userRegistration.Name, userRegistration.Email, userRegistration.Address, userRegistration.Phone, userRegistration.Money, userRegistration.UserType);
                    break;
                case UserType.Premium:
                    newUser = new PremiumUser(userRegistration.Name, userRegistration.Email, userRegistration.Address, userRegistration.Phone, userRegistration.Money, userRegistration.UserType);
                    break;
            }

            if (newUser == null)
                throw new AppExceptionHandler("User type not supported");

            var saved = _userRepository.Create(newUser);
            var savedDto = _mapper.Map<UserDto>(saved);

            savedDto.UserId = newUser.Id.ToString();

            return savedDto;
        }
    }
}
