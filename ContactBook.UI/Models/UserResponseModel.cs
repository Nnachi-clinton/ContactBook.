using System.ComponentModel.DataAnnotations;

namespace ContactBook.UI.Models
{
    public class UserResponseModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.PhoneNumber)]

        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Id { get; set; }

        public string ImageUrl { get; set; }



    }
}
