
using System.ComponentModel.DataAnnotations;

namespace ContactBook.Model.ViewModels
{
    public class PutViewModel
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
        public string  UserName { get; set; }
        [Required]
        public string  FirstName { get; set; }
        [Required]
        public string  LastName { get; set; }


        //[Required]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }


      



    }
}
