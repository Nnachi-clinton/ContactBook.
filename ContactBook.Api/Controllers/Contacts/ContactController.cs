using ContactBook.Core.Services.Abstractions.IContacts;
using ContactBook.Model.ViewModels;
using ContactBook.Model.ViewModels.ContactRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook.Api.Controllers.Contacts
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _IcontactService;

        public ContactController(IContactService icontactService)
        {
            _IcontactService = icontactService;
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            return await _IcontactService.DeleteUserAsync(id);
        }
        [Authorize]
        [HttpPost("Create-contact")]
        public async Task<IActionResult> AddNewUser([FromBody] ContactViewModel model, string UserId)
        {
            return await _IcontactService.CreateUserAsync(model, UserId);
        }

        [HttpGet("Get-contact-By-Id")]
        public async Task<Response<ContactResponseModel>> GetUserById(string id)
        {
            return await _IcontactService.FindUserByIdAsync(id);
        }
    }
}
