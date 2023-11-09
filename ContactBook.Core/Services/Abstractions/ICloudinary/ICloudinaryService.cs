using ContactBook.Model.Entity;
using Microsoft.AspNetCore.Http;

namespace ContactBook.Core.Services.Abstractions.ICloudinary
{
    public interface ICloudinaryService
    {
       

        Task<string> UploadImageAsync(User user, IFormFile image);
    }
}
