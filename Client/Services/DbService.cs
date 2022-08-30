using TestCoreHosted.Client.Data;
using TestCoreHosted.Shared.Helpes;
using TestCoreHosted.Shared.Models;
using Microsoft.AspNetCore.Components;
using TestCoreHosted.Client.IServices;

namespace TestCoreHosted.Client.Services
{
    public class DbService : IDb
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly RestFullService<Db> restClient;
        private readonly string uri = string.Empty;
        private readonly NavigationManager navigationManager;
        public DbService(HttpClient client, IConfiguration configuration, NavigationManager navigationManager)
        {
            _client = client;
            _configuration = configuration;
            uri = Constantes.dbs;
            this.navigationManager = navigationManager;
            restClient = new RestFullService<Db>(_client, _configuration, uri);
        }
        public async Task<bool> Delete(int id)
        {
            return await restClient.Delete(id);
        }

        public async Task<List<Db>> GetAsync()
        {
            return await restClient.GetAsync();
        }

        public async Task<Db> GetAsyncItem(int id)
        {
            return await restClient.GetById(id);
        }

        public async Task<Db> PostAsync(Db Db)
        {
            return await restClient.PostAsync(Db);
        }

        public async Task<Db> PutAsync(int Id, Db Db)
        {
            return await restClient.PutAsync(Id, Db);
        }

        public async Task<Db> VerifDataByString(string data)
        {
            return await restClient.GetDataByString(data);
        }
    }
}
