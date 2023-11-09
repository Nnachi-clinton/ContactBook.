using System.ComponentModel.DataAnnotations;

namespace ContactBook.Model.ViewModels.ContactRequest
{
    public class ContactViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
