using ContactBook.Model.Entity;
using ContactBook.Model.ViewModels;
using ContactBook.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook.Core.Services.Abstractions.ICrud
{
    public interface IRouteServices
    {
        public Task<IActionResult> CreateUserAsync(PostNewUserViewModel model);

        public Task<IActionResult> DeleteUserAsync(string id);

        public Task<Response<LoginResponseViewModel>> FindUserByIdAsync2(string id);

        public Task<LoginResponseViewModel> FindUserByIdAsync(string id);
        Task<List<UserResponseModel>> GetUsersAsync(int page, int pageSize);

        public Task<IActionResult> UpdateUserAsync(string id, PutViewModel model);

        public Task<IActionResult> GetUsersAsync2(int page, int pageSize);

        public Task<List<User>> SearchUsersAsync(string searchTerm);
    }
}
