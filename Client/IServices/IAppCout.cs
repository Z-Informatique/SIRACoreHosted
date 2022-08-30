using TestCoreHosted.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCoreHosted.Client.IServices
{
    public interface IAppCout
    {
        public Task<List<AppCout>> GetAsync();
        public Task<AppCout> GetAsyncItem(int id);
        public Task<AppCout> VerifDataByString(string data);
        public Task<bool> Delete(int id);
        public Task<AppCout> PostAsync(AppCout beneficiaire);
        public Task<AppCout> PutAsync(int Id, AppCout beneficiaire);
    }
}
