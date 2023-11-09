using ContactBook.Common.Authentication;
using ContactBook.Core.Services.Abstractions.IAuthentication;
using ContactBook.Model.Entity;
using ContactBook.Model.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ContactBook.Core.Services.Implementations.Authentication
{
    public class UserLoginService : IUserLoginService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;


        public UserLoginService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;            
            _configuration = configuration;
        }

        public async Task<Response<LoginResponseViewModel>> FindUserByEmailAsync(LoginRequestViewModel model)
        {
            var response = new Response<LoginResponseViewModel>();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return  response.Failed("User not found") ;
            }
            var validatePassword = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);
            if (!validatePassword.Succeeded)
            {
                return response.Failed("You are not authorized", StatusCodes.Status401Unauthorized);
            }
            string role;             
            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                role = "Admin";
            }
            else
            {
                role = "Regular";
            }        
            var token = JWT.GenerateJwtToken(user, role, _configuration);
            var result = new LoginResponseViewModel
            {                
                UserName = user.UserName,
                Id = user.Id,
                Token = token,
                FirstName = user.FirstName, 
                LastName = user.LastName,
            };
            return response.Success("User logged in successfully", StatusCodes.Status200OK, result);
        }

        public async Task<SignInResult> CheckPasswordSignInAsync(User user, string password, bool lockoutOnFailure)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
        }

    }

}
