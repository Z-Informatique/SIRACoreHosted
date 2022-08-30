using Microsoft.Extensions.Configuration;
using TestCoreHosted.Client.Data;
using TestCoreHosted.Shared.Helpes;
using TestCoreHosted.Shared.Models;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TestCoreHosted.Client.IServices;
using System.Collections.Generic;


namespace TestCoreHosted.Client.Services
{
    public class VersionDbService : IVersionDb
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly RestFullService<VersionDb> restClient;
        private readonly string uri = string.Empty;
        private readonly NavigationManager navigationManager;
        public VersionDbService(HttpClient client, IConfiguration configuration, NavigationManager navigationManager)
        {
            _client = client;
            _configuration = configuration;
            uri = Constantes.versiondbs;
            this.navigationManager = navigationManager;
            restClient = new RestFullService<VersionDb>(_client, _configuration, uri);
        }
        public async Task<bool> Delete(int id)
        {
            return await restClient.Delete(id);
        }

        public async Task<List<VersionDb>> GetAsync()
        {
            return await restClient.GetAsync();
        }

        public async Task<VersionDb> GetAsyncItem(int id)
        {
            return await restClient.GetById(id);
        }

        public async Task<List<VersionDb>> getItemsByMoteurDb(int IdDb)
        {
            return await restClient.GetDataListByParameter($"getItemsByMoteurDb?IdDb={IdDb}");
        }

        public async Task<VersionDb> PostAsync(VersionDb VersionDb)
        {
            return await restClient.PostAsync(VersionDb);
        }

        public async Task<VersionDb> PutAsync(int Id, VersionDb VersionDb)
        {
            return await restClient.PutAsync(Id, VersionDb);
        }

        public async Task<VersionDb> VerifDataByString(string data)
        {
            return await restClient.GetDataByString(data);
        }
    }
}
