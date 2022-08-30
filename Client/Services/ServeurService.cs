using TestCoreHosted.Client.Data;
using TestCoreHosted.Shared.Helpes;
using TestCoreHosted.Shared.Models;
using Microsoft.AspNetCore.Components;
using TestCoreHosted.Client.IServices;

namespace TestCoreHosted.Client.Services
{
    public class ServeurService : IServeur
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly RestFullService<Serveur> restClient;
        private readonly string uri = string.Empty;
        private readonly NavigationManager navigationManager;
        public ServeurService(HttpClient client, IConfiguration configuration, NavigationManager navigationManager)
        {
            _client = client;
            _configuration = configuration;
            uri = Constantes.serveurs;
            this.navigationManager = navigationManager;
            restClient = new RestFullService<Serveur>(_client, _configuration, uri);
        }
        public async Task<bool> Delete(int id)
        {
            return await restClient.Delete(id);
        }

        public async Task<List<Serveur>> GetAsync()
        {
            return await restClient.GetAsync();
        }

        public async Task<Serveur> GetAsyncItem(int id)
        {
            return await restClient.GetById(id);
        }

        public async Task<Serveur> PostAsync(Serveur Serveur)
        {
            return await restClient.PostAsync(Serveur);
        }

        public async Task<Serveur> PutAsync(int Id, Serveur Serveur)
        {
            return await restClient.PutAsync(Id, Serveur);
        }

        public async Task<Serveur> VerifDataByString(string data)
        {
            return await restClient.GetDataByString(data);
        }
    }
}
