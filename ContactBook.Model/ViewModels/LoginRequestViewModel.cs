

using System.ComponentModel.DataAnnotations;

namespace ContactBook.Model.ViewModels
{
    public class LoginRequestViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
