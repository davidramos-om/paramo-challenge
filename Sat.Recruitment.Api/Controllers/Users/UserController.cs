using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Common;
using Sat.Recruitment.Core.Services.Users;
using Sat.Recruitment.InfraEstructure.Models.DTOs;

namespace Sat.Recruitment.Api.Controllers.Users
{
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(List<UserDto>), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        public IActionResult GetAll()
        {
            var result = _userService.GetAll();
            return Ok(result);
        }

        [HttpGet("email")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        public IActionResult GetByEmail([FromQuery] string email)
        {
            var result = _userService.FindByEmail(email);
            if (result == null)
                return NotFound("User not found");

            return Ok(result);
        }

        [HttpPost()]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        public ActionResult RegisterUser([FromBody] RegisterUserDto model)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.Select(e => string.Join(", ", e.Errors.Select(m => m.ErrorMessage))));
                return BadRequest(errors);
            }

            try
            {
                var item = _userService.RegisterUser(model);
                return Ok(item);
            }
            catch (Exception ex)
            {
                if (ex is AppExceptionHandler)
                    return BadRequest(ex.Message);

                return StatusCode(500, ex.Message);
            }
        }
    }
}
