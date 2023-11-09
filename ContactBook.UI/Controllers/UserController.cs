using ContactBook.Model.ViewModels;
using ContactBook.UI.Models;
using ContactBook.UI.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService iUserService)
        {          
            _userService = iUserService;                   
        }       


        public async Task<IActionResult> GetAll()
        {
            List<UserResponseModel> response = await _userService.GetAllUsersAsync();
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(string Id)
        {
            var userResponseModel = await _userService.GetUserByIdAsync(Id);
            return View(userResponseModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostUser postUser)
        {
            bool createdSuccessfully = await _userService.CreateUserAsync(postUser);
            if (createdSuccessfully)
            {
                ViewBag.msg = "Submitted Successfully";
                ModelState.Clear();
            }
            else
            {
                ViewBag.msg = "Oops... Something went wrong";
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            var userResponseModel = await _userService.GetUserByIdAsync(Id);
            return View(userResponseModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LoginResponseViewModel updateUserModel, string Id)
        {
            bool updatedSuccessfully = await _userService.UpdateUserAsync(Id, updateUserModel);

            if (updatedSuccessfully)
            {
                ViewBag.msg = "Updated Successfully";
                ModelState.Clear();
            }
            else
            {
                ViewBag.msg = "Oops... Something went wrong";
            }

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            var userResponseModel = await _userService.GetUserByIdAsync(Id);
            return View(userResponseModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(LoginResponseViewModel updateUserModel, string Id)
        {
            bool deletedSuccessfully = await _userService.DeleteUserAsync(Id);

            if (deletedSuccessfully)
            {
                ViewBag.msg = "Deleted Successfully";
                ModelState.Clear();
            }
            else
            {
                ViewBag.msg = "Oops... Something went wrong";
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SearchUsers(string searchQuery)
        {
            var searchResults = await _userService.SearchUsersAsync(searchQuery);

            if (searchResults.Any())
            {
                return View(searchResults);
            }
            else
            {
                ViewBag.Message = "No matching users found.";
            }

            return View();
        }
    }
}
