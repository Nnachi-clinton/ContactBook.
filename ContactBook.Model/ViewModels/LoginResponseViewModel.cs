namespace ContactBook.Model.ViewModels
{
    public class LoginResponseViewModel : RegisterResponseViewModel
    {
        public string Token { get; set; }
        public string ConfirmEmail { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }


    }
}
