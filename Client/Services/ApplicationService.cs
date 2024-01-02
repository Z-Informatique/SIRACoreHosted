using TestCoreHosted.Client.Data;
using TestCoreHosted.Shared.Helpes;
using TestCoreHosted.Shared.Models;
using Microsoft.AspNetCore.Components;
using TestCoreHosted.Client.IServices;

namespace TestCoreHosted.Client.Services
{
    public partial class ApplicationService : IApplications
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly RestFullService<Application> restClient;
        private readonly string uri = string.Empty;
        private readonly NavigationManager navigationManager;
        public ApplicationService(HttpClient client, IConfiguration configuration, NavigationManager navigationManager)
        {
            _client = client;
            _configuration = configuration;
            uri = Constantes.applications;
            this.navigationManager = navigationManager;
            restClient = new RestFullService<Application>(_client, _configuration, uri);
        }
        public async Task<bool> Delete(int id)
        {
            return await restClient.Delete(id);
        }

        public async Task<List<Application>> GetAsync()
        {
            return await restClient.GetAsync();
        }

        public async Task<int> CountDataByLocation(string location)
        {
            return await restClient.CountAllData($"CountAppsByLocation?location={location}");
        }

        public async Task<Application> GetAsyncItem(int id)
        {
            return await restClient.GetById(id);
        }

        public async Task<Application> PostAsync(Application Application)
        {
            return await restClient.PostAsync(Application);
        }

        public async Task<Application> PutAsync(int Id, Application Application)
        {
            return await restClient.PutAsync(Id, Application);
        }

        public async Task<Application> VerifDataByString(string data)
        {
            return await restClient.GetDataByString(data);
        }

        public async Task<List<Application>> GetListeAsync()
        {
            return await restClient.GetDataListByParameter("getListe");
        }

        public async Task<KeyValuePair<string, int>[]> getStatistiqueByLocation()
        {
            return await restClient.getDataStatistique($"getStatistiqueByLocation");
        }
        public async Task<KeyValuePair<string, int>[]> getStatistiqueByDomaine()
        {
            return await restClient.getDataStatistique($"getStatistiqueByDomaine");
        }
    }
}
