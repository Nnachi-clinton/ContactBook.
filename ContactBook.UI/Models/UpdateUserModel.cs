using System.ComponentModel.DataAnnotations;

namespace ContactBook.UI.Models
{
    public class UpdateUserModel
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Compare(nameof(Email))]
        [EmailAddress]
        public string ConfirmEmail { get; set; }

        [Required]
        public string UserName { get; set; }

    }
}
