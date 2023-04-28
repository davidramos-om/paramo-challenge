using NSubstitute;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Sat.Recruitment.Api.Controllers.Users;
using Sat.Recruitment.Core.Services.Users;
using Sat.Recruitment.Common;
using Sat.Recruitment.InfraEstructure.Models.DTOs;

using Assert = Xunit.Assert;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserControllerTest
    {
        private readonly UserController _controller;
        private readonly IUserService mockUserService;

        public UserControllerTest()
        {
            mockUserService = Substitute.For<IUserService>();
            _controller = new UserController(mockUserService);
        }

        [Fact]
        public void CreateUser()
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

            // Act
            var result = _controller.RegisterUser(registerUserDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Post_InvalidDto_ReturnsBadRequest()
        {
            // Arrange
            RegisterUserDto registerUserDto = new()
            {
                Name = "David",
                Email = "",
                Address = "3-4 Ave. SPS, Cortes",
                Phone = "504-1111-0000",
                UserType = UserType.Premium,
                Money = 500
            };

            _controller.ModelState.AddModelError("Email", "Email is required");

            // Act
            var result = _controller.RegisterUser(registerUserDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void FindByEmail_Returns_NotFound_IfUserDoesNotExist()
        {
            // Arrange
            var email = "anyemailgoeshere@gmail.com";
            mockUserService.FindByEmail(email).Returns((UserDto)null);

            // Act
            var response =  _controller.GetByEmail(email);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public void FindByEmail_Returns_Ok_IfUserExist()
        {
            // Arrange
            var userEmail = "Agustina@gmail.com";  
            UserDto expectedUser = new()
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Garay y Otra Calle",
                Phone = "534645213542",
                UserType = UserType.Normal,
                Money = 100
            };

            mockUserService.FindByEmail(userEmail).Returns(expectedUser);

            // Act
            var response = _controller.GetByEmail(userEmail);
            var okResult = response as Microsoft.AspNetCore.Mvc.OkObjectResult;


            // Assert
            Assert.NotNull(response);
            Assert.NotNull(okResult);
            Assert.IsType<OkObjectResult>(okResult);
            
            var result = (OkObjectResult)response;
            var returnedUser = result.Value as UserDto;

            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(expectedUser, returnedUser);
        }
    }
}
