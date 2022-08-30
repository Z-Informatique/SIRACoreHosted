using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.IServices
{
    public interface IDb
    {
        public Task<List<Db>> GetAsync();
        public Task<Db> GetAsyncItem(int id);
        public Task<Db> VerifDataByString(string data);
        public Task<bool> Delete(int id);
        public Task<Db> PostAsync(Db Db);
        public Task<Db> PutAsync(int Id, Db Db);
    }
}
