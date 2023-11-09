using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using ContactBook.Core.Services.Abstractions.ICloudinary;
using Microsoft.AspNetCore.Http;
using ContactBook.Model.Entity;

namespace ContactBook.Core.Services.Implementations
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<string> UploadImageAsync(User user, IFormFile image)
        {

            if (image == null || image.Length <= 0)
            {
                return null;
            }

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(image.FileName, image.OpenReadStream())
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            user.ImageUrl = uploadResult.SecureUrl.AbsoluteUri;

            return user.ImageUrl;
        }
    }

}
