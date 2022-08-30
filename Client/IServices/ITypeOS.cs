using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.IServices
{
    public interface ITypeOS
    {
        public Task<List<TypeO>> GetAsync();
        public Task<TypeO> GetAsyncItem(int id);
        public Task<TypeO> VerifDataByString(string data);
        public Task<bool> Delete(int id);
        public Task<TypeO> PostAsync(TypeO TypeO);
        public Task<TypeO> PutAsync(int Id, TypeO TypeO);
    }
}
