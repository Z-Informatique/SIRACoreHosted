using Microsoft.AspNetCore.Components;
using TestCoreHosted.Client.Data;
using TestCoreHosted.Client.IServices;
using TestCoreHosted.Shared.Helpes;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.Services
{
    public class BAnalyticService : IBAnalytic
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly RestFullService<Banalytic> restClient;
        private readonly string uri = string.Empty;
        private readonly NavigationManager navigationManager;
        public BAnalyticService(HttpClient client, IConfiguration configuration, NavigationManager navigationManager)
        {
            _client = client;
            _configuration = configuration;
            uri = Constantes.BAnalytics;
            this.navigationManager = navigationManager;
            restClient = new RestFullService<Banalytic>(_client, _configuration, uri);
        }
        public async Task<bool> Delete(int id)
        {
            return await restClient.Delete(id);
        }

        public async Task<List<Banalytic>> GetAsync()
        {
            return await restClient.GetAsync();
        }

        public async Task<Banalytic> GetAsyncItem(int id)
        {
            return await restClient.GetById(id);
        }

        public async Task<Banalytic> PostAsync(Banalytic BAnalytic)
        {
            return await restClient.PostAsync(BAnalytic);
        }

        public async Task<Banalytic> PutAsync(int Id, Banalytic BAnalytic)
        {
            return await restClient.PutAsync(Id, BAnalytic);
        }

        public async Task<Banalytic> VerifDataByString(string data)
        {
            return await restClient.GetDataByString(data);
        }
    }
}
