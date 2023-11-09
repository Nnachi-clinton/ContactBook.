using ContactBook.Core.Services.Abstractions.IAuthentication;
using ContactBook.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace ContactBook.Api.Controllers.Authentications
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly IUserLoginService _userLoginUser;

        public AuthenticationController(IUserRegistrationService userRegistrationService, IUserLoginService userLoginUser)
        {
            _userRegistrationService = userRegistrationService;
            _userLoginUser = userLoginUser;
        }
               

        [HttpPost("Register-User")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userRegistrationService.RegisterAsync(model, "Regular", ModelState);

            if (result != null) 
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new { Message = "Failed to create Regular user." });
            }
        }

        [HttpPost("Register-Admin")]
        public async Task<IActionResult> CreateAdmin([FromBody] RegisterViewModel model)
        {
            var result = await _userRegistrationService.RegisterAsync(model, "Admin", ModelState);

            if (result != null) 
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(new { Message = "Failed to create admin user." });
            }
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _userLoginUser.FindUserByEmailAsync(model);
            return Ok(response);


        }


    }
}
