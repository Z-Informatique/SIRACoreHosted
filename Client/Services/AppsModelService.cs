using Microsoft.AspNetCore.Components;
using TestCoreHosted.Client.Data;
using TestCoreHosted.Client.IServices;
using TestCoreHosted.Shared.Helpes;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.Services
{
    public class AppsModelService : IAppsModel
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly RestFullService<AppsModel> restClient;
        private readonly string uri = string.Empty;
        private readonly NavigationManager navigationManager;
        public AppsModelService(HttpClient client, IConfiguration configuration, NavigationManager navigationManager)
        {
            _client = client;
            _configuration = configuration;
            uri = Constantes.applications;
            this.navigationManager = navigationManager;
            restClient = new RestFullService<AppsModel>(_client, _configuration, uri);
        }
        public async Task<List<AppsModel>> GetAppsModel()
        {
            return await restClient.GetDataListByParameter($"GetAppsModel");
        }
    }
}
