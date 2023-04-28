using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Core.DTOs;
using Sat.Recruitment.Common;
using Sat.Recruitment.Core.Interfaces;

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
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAll();
            return Ok(result);
        }

        [HttpGet("email")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            var result = await _userService.FindByEmail(email);
            if (result == null)
                return NotFound("User not found");

            return Ok(result);
        }

        [HttpPost()]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        public async Task<ActionResult> Create([FromBody] RegisterUserDto model)
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(", ", ModelState.Values.Select(e => string.Join(", ", e.Errors.Select(m => m.ErrorMessage))));
                return BadRequest(errors);
            }

            try
            {
                var item = await _userService.RegisterUser(model);
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
