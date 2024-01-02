using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.IServices
{
    public interface IApplications
    {
        public Task<List<Application>> GetAsync();
        public Task<List<Application>> GetListeAsync();
        public Task<int> CountDataByLocation(string location);
        public Task<KeyValuePair<string, int>[]> getStatistiqueByLocation();
        public Task<KeyValuePair<string, int>[]> getStatistiqueByDomaine();
        public Task<Application> GetAsyncItem(int id);
        public Task<Application> VerifDataByString(string data);
        public Task<bool> Delete(int id);
        public Task<Application> PostAsync(Application Application);
        public Task<Application> PutAsync(int Id, Application Application);
    }
}
