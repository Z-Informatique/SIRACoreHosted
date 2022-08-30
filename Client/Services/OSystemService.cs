using TestCoreHosted.Client.Data;
using TestCoreHosted.Shared.Helpes;
using TestCoreHosted.Shared.Models;
using Microsoft.AspNetCore.Components;
using TestCoreHosted.Client.IServices;

namespace TestCoreHosted.Client.Services
{
    public class OSystemService : IOSystems
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly RestFullService<OSystem> restClient;
        private readonly string uri = string.Empty;
        private readonly NavigationManager navigationManager;
        public OSystemService(HttpClient client, IConfiguration configuration, NavigationManager navigationManager)
        {
            _client = client;
            _configuration = configuration;
            uri = Constantes.osystems;
            this.navigationManager = navigationManager;
            restClient = new RestFullService<OSystem>(_client, _configuration, uri);
        }
        public async Task<bool> Delete(int id)
        {
            return await restClient.Delete(id);
        }

        public async Task<List<OSystem>> GetAsync()
        {
            return await restClient.GetAsync();
        }

        public async Task<OSystem> GetAsyncItem(int id)
        {
            return await restClient.GetById(id);
        }

        public async Task<List<OSystem>> GetAsyncItemByIdTypeOs(int IdTypeOs)
        {
            return await restClient.GetDataListByParameter($"GetAsyncItemByIdTypeOs?IdTypeOs={IdTypeOs}");
        }

        public async Task<OSystem> PostAsync(OSystem OSystem)
        {
            return await restClient.PostAsync(OSystem);
        }

        public async Task<OSystem> PutAsync(int Id, OSystem OSystem)
        {
            return await restClient.PutAsync(Id, OSystem);
        }

        public async Task<OSystem> VerifDataByString(string data)
        {
            return await restClient.GetDataByString(data);
        }
    }
}
