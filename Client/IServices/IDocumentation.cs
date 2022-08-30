using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.IServices
{
    public interface IDocumentation
    {
        public Task<List<Documentation>> GetAsync();
        public Task<List<Documentation>> GetAsyncByAppId(int IdApp);
        public Task<Documentation> GetAsyncItem(int id);
        public Task<Documentation> VerifDataByString(string data);
        public Task<bool> Delete(int id);
        public Task<Documentation> PostAsync(Documentation Documentation);
        public Task<Documentation> PutAsync(int Id, Documentation Documentation);
    }
}
