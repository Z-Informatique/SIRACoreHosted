using Microsoft.AspNetCore.Components;
using TestCoreHosted.Client.Data;
using TestCoreHosted.Client.IServices;
using TestCoreHosted.Shared.Helpes;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.Services
{
    public class UsersService : IUsers
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly RestFullService<User> restClient;
        private readonly string uri = string.Empty;
        private readonly NavigationManager navigationManager;
        public UsersService(HttpClient client, IConfiguration configuration, NavigationManager navigationManager)
        {
            _client = client;
            _configuration = configuration;
            uri = Constantes.users;
            this.navigationManager = navigationManager;
            restClient = new RestFullService<User>(_client, _configuration, uri);
        }
        public async Task<User> ChangePassword(string email, string currentPassword, string password)
        {
            return await restClient.ChangePassword(email, currentPassword, password);
        }

        public async Task<bool> Delete(int id)
        {
            return await restClient.Delete(id);
        }

        public async Task<List<User>> GetAsync()
        {
            return await restClient.GetAsync();
        }

        public async Task<User> GetAsyncItem(int id)
        {
            return await restClient.GetById(id);
        }

        public async Task<List<User>> getUsersByRole(int IntRole)
        {
            return await restClient.GetDataByIdAsync("getUsersByRole", IntRole);
        }

        public async Task<User> Login(string email, string password)
        {
            return await restClient.Login(email, password);
        }
        public async Task<User> PostAsync(User user)
        {
            return await restClient.PostAsync(user);
        }

        public async Task<User> PutAsync(int Id, User user)
        {
            return await restClient.PutAsync(Id, user);
        }

        public async Task<User> VerifDataByString(string data)
        {
            return await restClient.GetDataByString(data);
        }
    }
}
