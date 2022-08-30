using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.IServices
{
    public interface IDatabase
    {
        public Task<List<DataBase>> GetAsync();
        public Task<DataBase> GetAsyncItem(int id);
        public Task<DataBase> VerifDataByString(string data);
        public Task<bool> Delete(int id);
        public Task<DataBase> PostAsync(DataBase DataBase);
        public Task<DataBase> PutAsync(int Id, DataBase DataBase);
    }
}
