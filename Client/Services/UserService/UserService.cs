using Email_Planner.Shared;
using System.Net.Http.Json;

namespace Email_Planner.Client.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;

        public event Action OnChange;

        public List<User> Users { get; set; } = new List<User>();

        public UserService(HttpClient http)
        {
            _http = http;
        }

        public async Task GetUsers()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<User>>>("api/user");
            Users = response.Data;
        }

        public async Task AddUser(User user)
        {
            var response = await _http.PostAsJsonAsync("api/user", user);
            Users = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<User>>>()).Data;
            //await GetUsers();
            OnChange.Invoke();
        }

        public async Task UpdateUser(User user)
        {
            var response = await _http.PutAsJsonAsync("api/user", user);
            Users = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<User>>>()).Data;
            //await GetUsers();
            OnChange.Invoke();
        }

        public async Task DeleteUser(int id)
        {
            var response = await _http.DeleteAsync($"api/user/{id}");
            Users = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<User>>>()).Data;
            //await GetUsers();
            OnChange.Invoke();
        }

        public User CreateNewUser()
        {
            var newUser = new User { IsNew = true, Editing = true};
            Users.Add(newUser);
            OnChange.Invoke();
            return newUser;
        }
    }
}
