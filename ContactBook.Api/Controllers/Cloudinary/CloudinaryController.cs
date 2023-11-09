using ContactBook.Core.Services.Abstractions.ICloudinary;
using ContactBook.Model.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook.Api.Controllers.Cloudinary
{
    [Route("api/[controller]")]
    [ApiController]
    public class CloudinaryController : ControllerBase
    {
        private readonly ICloudinaryService _cloudinaryService;
        private readonly UserManager<User> _userManager;

        public CloudinaryController(ICloudinaryService cloudinaryService, UserManager<User> userManager)
        {
            _cloudinaryService = cloudinaryService;
            _userManager = userManager;
        }


       // [Authorize(Roles ="Regular")]
        [HttpPatch("image/{id}")]
        public async Task<IActionResult> UpUserLoadImage(string id, IFormFile image)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }
            if (image == null)
            {
                return BadRequest(new { Message = "Image file is required" });
            }
            if (image.Length <= 0)
            {
                return BadRequest(new { Message = "Image file is empty" });
            }

            var imageUrl = await _cloudinaryService.UploadImageAsync(user, image);

            if (imageUrl == null)
            {
                return BadRequest(new { Message = "Failed to upload the image" });
            }

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                return BadRequest(new { Message = "Image update failed" });
            }

            return Ok(new { Message = "User image updated successfully", ImageUrl = imageUrl });
        }


    }
}
