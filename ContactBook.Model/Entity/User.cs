using Microsoft.AspNetCore.Identity;

namespace ContactBook.Model.Entity
{
    public class User : IdentityUser 
    {
        public string ImageUrl { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public ICollection<Contact> Contacts { get; set; }


    }
}
