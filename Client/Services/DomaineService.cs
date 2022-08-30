using TestCoreHosted.Client.Data;
using TestCoreHosted.Shared.Helpes;
using TestCoreHosted.Shared.Models;
using Microsoft.AspNetCore.Components;
using TestCoreHosted.Client.IServices;


namespace TestCoreHosted.Client.Services
{
    public class DomaineService : IDomaine
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly RestFullService<Domaine> restClient;
        private readonly string uri = string.Empty;
        private readonly NavigationManager navigationManager;
        public DomaineService(HttpClient client, IConfiguration configuration, NavigationManager navigationManager)
        {
            _client = client;
            _configuration = configuration;
            uri = Constantes.domaines;
            this.navigationManager = navigationManager;
            restClient = new RestFullService<Domaine>(_client, _configuration, uri);
        }
        public async Task<bool> Delete(int id)
        {
            return await restClient.Delete(id);
        }

        public async Task<List<Domaine>> GetAsync()
        {
            return await restClient.GetAsync();
        }

        public async Task<Domaine> GetAsyncItem(int id)
        {
            return await restClient.GetById(id);
        }

        public async Task<Domaine> PostAsync(Domaine Domaine)
        {
            return await restClient.PostAsync(Domaine);
        }

        public async Task<Domaine> PutAsync(int Id, Domaine Domaine)
        {
            return await restClient.PutAsync(Id, Domaine);
        }

        public async Task<Domaine> VerifDataByString(string data)
        {
            return await restClient.GetDataByString(data);
        }
    }
}
