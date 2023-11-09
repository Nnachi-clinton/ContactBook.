using ContactBook.Common.Validations;
using ContactBook.Core.Services.Abstractions.ICrud;
using ContactBook.Model.Entity;
using ContactBook.Model.ViewModels;
using ContactBook.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactBook.Core.Services.Implementations.Crud
{
    public class RouteServices : IRouteServices
    {
        private readonly UserManager<User> _userManager;
        private readonly UserValidator _userValidator;

        public RouteServices(UserManager<User> userManager, UserValidator userValidator)
        {
            _userManager = userManager;
            _userValidator = userValidator;
        }       
        public async Task<IActionResult> CreateUserAsync(PostNewUserViewModel model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                

            };
                       
            var validationResult = await _userValidator.ValidateUserAsync(user, model.Password);
            if (!validationResult.Succeeded)
            {
                var errors = validationResult.Errors.Select(e => e.Description).ToList();
                return new BadRequestObjectResult(new { Message = "Failed to create user", Errors = errors });
            }

         
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return new OkObjectResult(new { Message = "User created Successfully" });
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return new BadRequestObjectResult(new { Message = "Failed to create user", Errors = errors });
            }

        }
        public async Task<IActionResult> DeleteUserAsync(string userId)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return new NotFoundObjectResult(new { Message = "User not found." });
                }

                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    return new BadRequestObjectResult(new { Message = "Failed to delete user." });
                }

                return new OkObjectResult(new { Message = "User deleted successfully" });
            }                      
        public async Task<IActionResult> GetUsersAsync2(int page, int pageSize)
            {
                var totalUsers = await _userManager.Users.CountAsync();
                var totalPages = (int)Math.Ceiling(totalUsers / (double)pageSize);
                page = Math.Max(1, Math.Min(totalPages, page));

                var users = await _userManager.Users
                    .OrderBy(u => u.Id)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var paginatedResult = new PaginatedUserViewModel
                {
                    TotalUsers = totalUsers,
                    CurrentPage = page,
                    PageSize = pageSize,
                    Users = users
                };

                return new OkObjectResult(paginatedResult);
            }
        public async Task<List<UserResponseModel>> GetUsersAsync(int page, int pageSize)
        {
            var totalUsers = await _userManager.Users.CountAsync();
            var totalPages = (int)Math.Ceiling(totalUsers / (double)pageSize);
            page = Math.Max(1, Math.Min(totalPages, page));

            var users = await _userManager.Users
                .OrderBy(u => u.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var userlist = new List<UserResponseModel>();
            foreach (var user in users)
            {
                var userResponseModel = new UserResponseModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,
                    ImageUrl = user.ImageUrl,
                    
                };
                userlist.Add(userResponseModel);
            }

            return userlist;
        }
        public async Task<List<User>> SearchUsersAsync(string searchTerm)
        {
            try
            {
                var result = await _userManager.Users
                .Where(u => u.UserName.Contains(searchTerm) || u.Email.Contains(searchTerm) || u.Id.Contains(searchTerm)
                || u.PhoneNumber.Contains(searchTerm)).ToListAsync();

                return result;
            }
            catch (Exception ex)
            {

                throw new ApplicationException(ex.Message);
            }
            
        }        
        public async Task<Response<LoginResponseViewModel>> FindUserByIdAsync2(string id)
        {
            var response = new Response<LoginResponseViewModel>();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return response.Failed("User not found", StatusCodes.Status404NotFound);
            }

            var result = new LoginResponseViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Id = user.Id,
            };

            return response.Success("User found",StatusCodes.Status200OK,result);

        }
        public async Task<LoginResponseViewModel> FindUserByIdAsync(string id)
        {
            var response = new Response<LoginResponseViewModel>();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            var result = new LoginResponseViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Id = user.Id,
            };

            return result;

        }
        public async Task<IActionResult> UpdateUserAsync(string id, PutViewModel model)
        {
            

            // Find the user by ID
             var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return new NotFoundObjectResult(new { Message = "User not found" });
            }

            
            user.Email = model.Email;
            user.UserName = model.UserName;
            user.PhoneNumber = model.PhoneNumber;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
          
            
                       
            var validationResult = await _userValidator.ValidateUserAsync(user);
            if (!validationResult.Succeeded)
            {
                var errors = validationResult.Errors.Select(e => e.Description).ToList();
                return new BadRequestObjectResult(new { Message = "Validation failed", Errors = errors });
            }
           

            
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return new BadRequestObjectResult(new 
                { 
                    Message = "Failed to update user", updateResult.Errors 
                });
            }

            return new OkObjectResult(new { Message = "User updated successfully" });
        }

    }
}
