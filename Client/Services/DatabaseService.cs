using TestCoreHosted.Client.Data;
using TestCoreHosted.Shared.Helpes;
using TestCoreHosted.Shared.Models;
using Microsoft.AspNetCore.Components;
using TestCoreHosted.Client.IServices;

namespace TestCoreHosted.Client.Services
{
    public class DatabaseService : IDatabase
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly RestFullService<DataBase> restClient;
        private readonly string uri = string.Empty;
        private readonly NavigationManager navigationManager;
        public DatabaseService(HttpClient client, IConfiguration configuration, NavigationManager navigationManager)
        {
            _client = client;
            _configuration = configuration;
            uri = Constantes.databases;
            this.navigationManager = navigationManager;
            restClient = new RestFullService<DataBase>(_client, _configuration, uri);
        }
        public async Task<bool> Delete(int id)
        {
            return await restClient.Delete(id);
        }

        public async Task<List<DataBase>> GetAsync()
        {
            return await restClient.GetAsync();
        }

        public async Task<DataBase> GetAsyncItem(int id)
        {
            return await restClient.GetById(id);
        }

        public async Task<DataBase> PostAsync(DataBase DataBase)
        {
            return await restClient.PostAsync(DataBase);
        }

        public async Task<DataBase> PutAsync(int Id, DataBase DataBase)
        {
            return await restClient.PutAsync(Id, DataBase);
        }

        public async Task<DataBase> VerifDataByString(string data)
        {
            return await restClient.GetDataByString(data);
        }
    }
}
