using TestCoreHosted.Client.Data;
using TestCoreHosted.Shared.Helpes;
using TestCoreHosted.Shared.Models;
using Microsoft.AspNetCore.Components;
using TestCoreHosted.Client.IServices;

namespace TestCoreHosted.Client.Services
{
    public class DocumentationService : IDocumentation
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly RestFullService<Documentation> restClient;
        private readonly string uri = string.Empty;
        private readonly NavigationManager navigationManager;
        public DocumentationService(HttpClient client, IConfiguration configuration, NavigationManager navigationManager)
        {
            _client = client;
            _configuration = configuration;
            uri = Constantes.documentations;
            this.navigationManager = navigationManager;
            restClient = new RestFullService<Documentation>(_client, _configuration, uri);
        }
        public async Task<bool> Delete(int id)
        {
            return await restClient.Delete(id);
        }

        public async Task<List<Documentation>> GetAsync()
        {
            return await restClient.GetAsync();
        }

        public async Task<List<Documentation>> GetAsyncByAppId(int IdApp)
        {
            return await restClient.GetDataListByParameter($"GetAsyncByAppId?IdApp={IdApp}");
        }

        public async Task<Documentation> GetAsyncItem(int id)
        {
            return await restClient.GetById(id);
        }

        public async Task<Documentation> PostAsync(Documentation Documentation)
        {
            return await restClient.PostAsync(Documentation);
        }

        public async Task<Documentation> PutAsync(int Id, Documentation Documentation)
        {
            return await restClient.PutAsync(Id, Documentation);
        }

        public async Task<Documentation> VerifDataByString(string data)
        {
            return await restClient.GetDataByString(data);
        }
    }
}
