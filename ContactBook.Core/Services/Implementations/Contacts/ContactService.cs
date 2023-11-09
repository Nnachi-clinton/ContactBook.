using ContactBook.Common.Validations;
using ContactBook.Core.Services.Abstractions.IContacts;
using ContactBook.Data.DbContext;
using ContactBook.Model.Entity;
using ContactBook.Model.ViewModels;
using ContactBook.Model.ViewModels.ContactRequest;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ContactBook.Core.Services.Implementations.Contacts
{
    public class ContactService : IContactService
    {
        private readonly MyDbContext _myDbContext;
        private readonly UserManager<User> _usermanager;

        public ContactService(MyDbContext myDbContext, UserManager<User> usermanager)
        {
            _myDbContext = myDbContext;
            _usermanager = usermanager;
        }

        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            var user = await _myDbContext.Contacts.FindAsync(id);
            if (user == null)
            {
                return new NotFoundObjectResult(new { Message = "User not found." });
            }

            _myDbContext.Contacts.Remove(user);
            await _myDbContext.SaveChangesAsync();
            return new OkObjectResult(new { Message = "User deleted successfully" });
        }

        public async Task<IActionResult> CreateUserAsync(ContactViewModel model, string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                return new BadRequestObjectResult(new { Message = "Id cannot be empty" });
            }            

            var idExists = await _usermanager.FindByIdAsync(Id) == null;

            if (idExists)
            {
                return new BadRequestObjectResult(new { Message = "Invalid Id" });
            }
            
            var contact = new Contact
            {
                Id = Guid.NewGuid().ToString(),
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,
                IsDeleted = false,
                 UserId = Id

            };


            var uniqueUser = await _myDbContext.Contacts.FirstOrDefaultAsync(x=>x.Email == model.Email);

            if (uniqueUser is not null)
            {
                return new BadRequestObjectResult(new { Message = "Email already exists" });
            }



            await _myDbContext.Contacts.AddAsync(contact);
            await _myDbContext.SaveChangesAsync();
            return new OkObjectResult(new { Message = "User created Successfully" });


        }


        public async Task<Response<ContactResponseModel>> FindUserByIdAsync(string id)
        {
            var response = new Response<ContactResponseModel>();
            var user = await _myDbContext.Contacts.FindAsync(id);

            if (user == null)
            {
                return response.Failed("User not found", StatusCodes.Status404NotFound);
            }

            var status = new ContactResponseModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Id = user.Id,
                Email = user.Email,

            };
            return response.Success("User found successfully", StatusCodes.Status200OK, status);

        }



    }
}
