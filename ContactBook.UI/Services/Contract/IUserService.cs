using ContactBook.Model.ViewModels;
using ContactBook.UI.Models;

namespace ContactBook.UI.Services.Contract
{
    public interface IUserService
    {
        Task<List<UserResponseModel>> GetAllUsersAsync();
        Task<LoginResponseViewModel> GetUserByIdAsync(string userId);
        Task<bool> CreateUserAsync(PostUser postUser);
        Task<bool> UpdateUserAsync(string userId, LoginResponseViewModel updateUserModel);
        Task<bool> DeleteUserAsync(string userId);
        Task<List<LoginResponseViewModel>> SearchUsersAsync(string searchQuery);
    }
}
