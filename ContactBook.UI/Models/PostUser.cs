using System.ComponentModel.DataAnnotations;

namespace ContactBook.UI.Models
{
    public class PostUser
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
        public string ConfirmPassword { get; set; }
    }
}
