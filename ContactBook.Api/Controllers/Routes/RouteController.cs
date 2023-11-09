using ContactBook.Core.Services.Abstractions.ICrud;
using ContactBook.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ContactBook.Api.Controllers.Crud
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteServices _services;

        public RouteController(IRouteServices services)
        {
            _services = services;
        }




        [HttpPost("add-new-user")]
        public async Task<IActionResult> AddNewUser([FromBody] PostNewUserViewModel model)
        {
            return await _services.CreateUserAsync(model);
        }



       // [Authorize(Roles ="Admin")]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            return await _services.DeleteUserAsync(id);
        }

        
        [HttpGet("Get-User-By-Id")]
        public async Task<IActionResult> GetUserById(string id)
        {
            return Ok(await _services.FindUserByIdAsync(id));
        }

        //[Authorize(Roles ="Regular")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] PutViewModel model)
        {
            var user = await _services.FindUserByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { Message = "User not found." });
            }


            return await _services.UpdateUserAsync(id, model);

          
        }

       // [Authorize(Roles ="Admin")]
        [HttpGet("search-term")]
        public async Task<IActionResult> SearchUsers(string searchQuery)
        {
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                return BadRequest("Search term is required.");
            }

            var users = await _services.SearchUsersAsync(searchQuery);

            return Ok(users);
        }


       // [Authorize(Roles ="Admin")]
        [HttpGet("all-users")]
       
        public async Task<IActionResult> GetAllUsers(int page = 1, int pageSize = 10)
        {
                return Ok(await _services.GetUsersAsync(page, pageSize));
        }

        

    }
}
