using TestCoreHosted.Client.Data;
using TestCoreHosted.Shared.Helpes;
using TestCoreHosted.Shared.Models;
using Microsoft.AspNetCore.Components;
using TestCoreHosted.Client.IServices;

namespace TestCoreHosted.Client.Services
{
    public class EnvService : IEnvironnement
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly RestFullService<Environnement> restClient;
        private readonly string uri = string.Empty;
        private readonly NavigationManager navigationManager;
        public EnvService(HttpClient client, IConfiguration configuration, NavigationManager navigationManager)
        {
            _client = client;
            _configuration = configuration;
            uri = Constantes.environnements;
            this.navigationManager = navigationManager;
            restClient = new RestFullService<Environnement>(_client, _configuration, uri);
        }
        public async Task<bool> Delete(int id)
        {
            return await restClient.Delete(id);
        }

        public async Task<List<Environnement>> GetAsync()
        {
            return await restClient.GetAsync();
        }

        public async Task<Environnement> GetAsyncItem(int id)
        {
            return await restClient.GetById(id);
        }

        public async Task<Environnement> PostAsync(Environnement Environnement)
        {
            return await restClient.PostAsync(Environnement);
        }

        public async Task<Environnement> PutAsync(int Id, Environnement Environnement)
        {
            return await restClient.PutAsync(Id, Environnement);
        }

        public async Task<Environnement> VerifDataByString(string data)
        {
            return await restClient.GetDataByString(data);
        }
    }
}
