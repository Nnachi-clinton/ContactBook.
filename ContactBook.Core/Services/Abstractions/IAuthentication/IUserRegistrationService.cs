using ContactBook.Model.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ContactBook.Core.Services.Abstractions.IAuthentication
{
    public interface IUserRegistrationService
    {

        //public  Task<string> RegisterUserAsync(RegisterViewModel model, ModelStateDictionary modelState);

        //public  Task<string> CreateAdminUserAsync(string email, string password, string phoneNumber);

        public Task<Response<RegisterResponseViewModel>> RegisterAsync(RegisterViewModel model, string roleName, ModelStateDictionary modelState);
    }
}
