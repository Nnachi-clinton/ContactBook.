
using System.ComponentModel.DataAnnotations;

namespace ContactBook.Model.ViewModels
{
    public class PostNewUserViewModel
    {

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }     


        [Required]
        [DataType(DataType.PhoneNumber)]

        public string PhoneNumber { get; set; }
      
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}

