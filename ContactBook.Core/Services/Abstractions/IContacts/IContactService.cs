using ContactBook.Model.ViewModels;
using ContactBook.Model.ViewModels.ContactRequest;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ContactBook.Core.Services.Abstractions.IContacts
{
    public interface IContactService
    {
        Task<IActionResult> DeleteUserAsync(string id);
        Task<IActionResult> CreateUserAsync(ContactViewModel model, string userid);
        Task<Response<ContactResponseModel>> FindUserByIdAsync(string id);
    }
}
