using ContactBook.Common.Authentication;
using ContactBook.Common.Validations;
using ContactBook.Core.Services.Abstractions.IAuthentication;
using ContactBook.Model.Entity;
using ContactBook.Model.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;

namespace ContactBook.Core.Services.Implementations.Authentication
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly UserValidator _userValidator;

        public UserRegistrationService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, UserValidator userValidator)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _userValidator = userValidator;
        }


        public async Task<Response<RegisterResponseViewModel>> RegisterAsync(RegisterViewModel model, string roleName, ModelStateDictionary modelState)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
            };

            var uniqueUser = await _userValidator.ValidateUserAsync(user, model.Password);
            var uniquePassword = await _userValidator.ValidateUserPasswordAsync(user, model.Password);
            var response = new Response<RegisterResponseViewModel>();
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!uniqueUser.Succeeded)
            {
                var errors = uniqueUser.Errors.Select(e => e.Description).ToList();
                return response.Failed("Email already exist", StatusCodes.Status400BadRequest);

            }
            
            if (!uniquePassword.Succeeded)
            {
                var errors = uniqueUser.Errors.Select(e => e.Description).ToList();
                return response.Failed("{Failed : PasswordRequiresNonAlphanumeric,PasswordRequiresDigit,PasswordRequiresUpper}", StatusCodes.Status400BadRequest);

            }
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(roleName))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                    await _userManager.AddToRoleAsync(user, roleName);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        modelState.AddModelError(string.Empty, error.Description);
                    }
                }

                var status = new RegisterResponseViewModel
                {
                    UserName = user.UserName
                };

                return response.Success($"{status.UserName} created successfully", StatusCodes.Status200OK);
        }      



    }
}
