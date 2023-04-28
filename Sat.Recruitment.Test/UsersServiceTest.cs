using AutoMapper;
using NSubstitute;
using Xunit;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
 
using Sat.Recruitment.Common;
using Sat.Recruitment.Core.Mapper;
using Sat.Recruitment.Core.Services.Users;
using Sat.Recruitment.InfraEstructure.Models.DTOs;

using Assert = Xunit.Assert;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UsersServiceTest
    {
        private readonly IUserService mockUserService; 

        public UsersServiceTest()
        {
            mockUserService = Substitute.For<IUserService>();            
        }

        [Fact]
        public void WhenUserDoestNotExists_Should_Return_Null()
        {
            // Arrange
            var email = "carlos@email.com";
            mockUserService.FindByEmail(email).ReturnsNull();

            // Act
            var result = mockUserService.FindByEmail(email);

            // Assert
            Assert.True(result == null);
        }

        [Fact]
        public void WhenUserDoesExist_Should_Return_True()
        {
            // Arrange
            var email = "Juan@marmol.com";
            UserDto juanUser = new()
            {
                UserId = Guid.NewGuid().ToString(),
                Name = "Juan",
                Email = "Juan@marmol.com",
                Address = "5491154762312",
                Phone = "Peru 2464",
                UserType = UserType.Normal,
                Money = 1234
            };

            mockUserService.FindByEmail(email).Returns(juanUser);

            // Act
            var result = mockUserService.FindByEmail(email);

            // Assert
            Assert.Equal(juanUser.Email, result?.Email);
        }

        [Fact]
        public void WhenUserEmailIsDuplicated_Should_Throw_Exception()
        {
            // Arrange
            RegisterUserDto userDto = new()
            {
                Name = "Juan",
                Email = "Juan@marmol.com",
                Address = "303-2992-22",
                Phone = "Peru 2464",
                UserType = UserType.Normal,
                Money = 100
            };

            mockUserService.RegisterUser(userDto).Returns(x => throw new AppExceptionHandler("Email already registered."));

            // Act
            UserDto act() => mockUserService.RegisterUser(userDto);

            // Assert
            Assert.Throws<AppExceptionHandler>(act);
        }


        [Fact]
        public void WhenUserPhoneIsDuplicated_Should_Throw_Exception()
        {
            // Arrange
            RegisterUserDto userDto = new()
            {
                Name = "Cristopher",
                Email = "crist@email.com",
                Address = "Zona 5, Ave. 6",
                Phone = "534645213542",
                UserType = UserType.Normal,
                Money = 100
            };

            mockUserService.RegisterUser(userDto).Returns(x => throw new AppExceptionHandler("Phone number already used by someone else."));

            // Act
            UserDto act() => mockUserService.RegisterUser(userDto);

            // Assert
            Assert.Throws<AppExceptionHandler>(act);
        }

        [Fact]
        public void WhenUserNameOrAdddressIsDuplicated_Should_Throw_Exception()
        {
            // Arrange
            RegisterUserDto userDto = new()
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Garay y Otra Calle",
                Phone = "534645213542",
                UserType = UserType.Normal,
                Money = 100
            };

            mockUserService.RegisterUser(userDto).Returns(x => throw new AppExceptionHandler("Already exists a user using the name and address entered."));

            // Act
            UserDto act() => mockUserService.RegisterUser(userDto);

            // Assert
            Assert.Throws<AppExceptionHandler>(act);
        }

        [Fact]
        public void CreateANewUser_IfDoesntExist()
        {
            // Arrange
            RegisterUserDto registerUserDto = new()
            {
                Name = "David",
                Email = "davidramos015@gmail.com",
                Address = "3-4 Ave. SPS, Cortes",
                Phone = "504-1111-0000",
                UserType = UserType.Premium,
                Money = 500
            };

            var mapperConfig = new MapperConfiguration(cfg => {
                cfg.AddProfile(new AutoMapperProfiles());
            });

            var mapper = mapperConfig.CreateMapper();
            var expectedUserDto = mapper.Map<UserDto>(registerUserDto);
           
            mockUserService.RegisterUser(registerUserDto).Returns(expectedUserDto);

            // Act
            var createdUserDto =  mockUserService.RegisterUser(registerUserDto);

            // Assert
            Assert.Equal(expectedUserDto, createdUserDto);
        }

        [Fact]
        public void RegisterUser_WithInvalidDto_Should_ThrowsException()
        {
            // Arrange
            RegisterUserDto registerUserDto = new()
            {
                Name = "",
                Email = "",
                Address = "",
                Phone = "504-1111-0000",
                UserType = UserType.None,
                Money = 500
            };
            mockUserService.RegisterUser(registerUserDto).Throws<AppExceptionHandler>();

            // Act
            var act = () => mockUserService.RegisterUser(registerUserDto);
            
            // Assert
            Assert.Throws<AppExceptionHandler>(act);
        }
    }
}