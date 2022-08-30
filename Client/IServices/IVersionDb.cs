using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.IServices
{
    public interface IVersionDb
    {
        public Task<List<VersionDb>> GetAsync();
        public Task<List<VersionDb>> getItemsByMoteurDb(int IdDb);
        public Task<VersionDb> GetAsyncItem(int id);
        public Task<VersionDb> VerifDataByString(string data);
        public Task<bool> Delete(int id);
        public Task<VersionDb> PostAsync(VersionDb VersionDb);
        public Task<VersionDb> PutAsync(int Id, VersionDb VersionDb);
    }
}
