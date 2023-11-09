using ContactBook.Model.Entity;
using ContactBook.Model.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;

namespace ContactBook.Core.Services.Abstractions.IAuthentication
{
    public interface IUserLoginService
    {
        Task<Response<LoginResponseViewModel>> FindUserByEmailAsync(LoginRequestViewModel model);
        Task<SignInResult> CheckPasswordSignInAsync(User user, string password, bool lockoutOnFailure);
    }
}
