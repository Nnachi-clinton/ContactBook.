
using System.ComponentModel.DataAnnotations;

namespace ContactBook.Model.ViewModels
{
    public class RegisterViewModel
    {

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Compare(nameof(Email))]
        [DataType(DataType.EmailAddress)]
        public string ConfirmEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [Compare(nameof(PhoneNumber))]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumberConfirmed { get; set; }

    }
}
