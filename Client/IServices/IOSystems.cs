using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.IServices
{
    public interface IOSystems
    {
        public Task<List<OSystem>> GetAsync();
        public Task<OSystem> GetAsyncItem(int id);
        public Task<List<OSystem>> GetAsyncItemByIdTypeOs(int IdTypeOs);
        public Task<OSystem> VerifDataByString(string data);
        public Task<bool> Delete(int id);
        public Task<OSystem> PostAsync(OSystem OSystem);
        public Task<OSystem> PutAsync(int Id, OSystem OSystem);
    }
}
