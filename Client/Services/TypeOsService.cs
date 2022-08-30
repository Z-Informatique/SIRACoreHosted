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
    public class TypeOsService : ITypeOS
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly RestFullService<TypeO> restClient;
        private readonly string uri = string.Empty;
        private readonly NavigationManager navigationManager;
        public TypeOsService(HttpClient client, IConfiguration configuration, NavigationManager navigationManager)
        {
            _client = client;
            _configuration = configuration;
            uri = Constantes.typeos;
            this.navigationManager = navigationManager;
            restClient = new RestFullService<TypeO>(_client, _configuration, uri);
        }
        public async Task<bool> Delete(int id)
        {
            return await restClient.Delete(id);
        }

        public async Task<List<TypeO>> GetAsync()
        {
            return await restClient.GetAsync();
        }

        public async Task<TypeO> GetAsyncItem(int id)
        {
            return await restClient.GetById(id);
        }

        public async Task<TypeO> PostAsync(TypeO TypeO)
        {
            return await restClient.PostAsync(TypeO);
        }

        public async Task<TypeO> PutAsync(int Id, TypeO TypeO)
        {
            return await restClient.PutAsync(Id, TypeO);
        }

        public async Task<TypeO> VerifDataByString(string data)
        {
            return await restClient.GetDataByString(data);
        }
    }
}
