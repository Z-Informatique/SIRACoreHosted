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
    public class MetierService : IMetier
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly RestFullService<Metier> restClient;
        private readonly string uri = string.Empty;
        private readonly NavigationManager navigationManager;
        public MetierService(HttpClient client, IConfiguration configuration, NavigationManager navigationManager)
        {
            _client = client;
            _configuration = configuration;
            uri = Constantes.metiers;
            this.navigationManager = navigationManager;
            restClient = new RestFullService<Metier>(_client, _configuration, uri);
        }
        public async Task<bool> Delete(int id)
        {
            return await restClient.Delete(id);
        }

        public async Task<List<Metier>> GetAsync()
        {
            return await restClient.GetAsync();
        }

        public async Task<Metier> GetAsyncItem(int id)
        {
            return await restClient.GetById(id);
        }

        public async Task<Metier> PostAsync(Metier Metier)
        {
            return await restClient.PostAsync(Metier);
        }

        public async Task<Metier> PutAsync(int Id, Metier Metier)
        {
            return await restClient.PutAsync(Id, Metier);
        }

        public async Task<Metier> VerifDataByString(string data)
        {
            return await restClient.GetDataByString(data);
        }
    }
}
