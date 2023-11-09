using ContactBook.Model.ViewModels;
using ContactBook.UI.Models;
using ContactBook.UI.Services.Contract;
using Newtonsoft.Json;
using System.Net.Http.Headers;
//using UserResponseModel = ContactBook.UI.Models.UserResponseModel;

namespace ContactBook.UI.Services.Implementation
{
    public class UserServices : IUserService
    {
        private readonly string BaseUrl = string.Empty;
        private readonly HttpClient _httpClient;
        public UserServices(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            BaseUrl = configuration.GetSection("ExternalUrl:ContactBook").Value;
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }

     
        public async Task<List<UserResponseModel>> GetAllUsersAsync()
        {
            List<UserResponseModel> response = new();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage result = await _httpClient.GetAsync(_httpClient.BaseAddress + "/Route/GetAllUsers/all-users");

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<List<UserResponseModel>>(content);
                }
            }

            return response;
        }
        public async Task<LoginResponseViewModel> GetUserByIdAsync(string userId)
        {
            var userResponseModel = new LoginResponseViewModel();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage result = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/Route/Getuserbyid/get-user-by-id?ID={userId}");

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    userResponseModel = JsonConvert.DeserializeObject<LoginResponseViewModel>(content);
                }
            }

            return userResponseModel;
        }
        public async Task<bool> CreateUserAsync(PostUser postUser)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage result = await _httpClient.PostAsJsonAsync($"{_httpClient.BaseAddress}/Route/AddNewUser/add-new-user", postUser);
                return result.IsSuccessStatusCode;
            }
        }
        public async Task<bool> UpdateUserAsync(string userId, LoginResponseViewModel updateUserModel)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage result = await _httpClient.PutAsJsonAsync($"{_httpClient.BaseAddress}/Route/UpdateUser/update?ID={userId}", updateUserModel);
                return result.IsSuccessStatusCode;
            }
        }
        public async Task<bool> DeleteUserAsync(string userId)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage result = await _httpClient.DeleteAsync($"{_httpClient.BaseAddress}/Route/DeleteUser/delete?ID={userId}");
                return result.IsSuccessStatusCode;
            }
        }
        public async Task<List<LoginResponseViewModel>> SearchUsersAsync(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                return new List<LoginResponseViewModel>();
            }

            var searchResults = new List<LoginResponseViewModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                HttpResponseMessage response = await client.GetAsync($"api/Route/SearchUsers/search-term?searchQuery={searchQuery}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    searchResults = JsonConvert.DeserializeObject<List<LoginResponseViewModel>>(data);
                }

                return searchResults;
            }
        }


    }
}
