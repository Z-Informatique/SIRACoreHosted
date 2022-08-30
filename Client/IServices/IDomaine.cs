using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.IServices
{
    public interface IDomaine
    {
        public Task<List<Domaine>> GetAsync();
        public Task<Domaine> GetAsyncItem(int id);
        public Task<Domaine> VerifDataByString(string data);
        public Task<bool> Delete(int id);
        public Task<Domaine> PostAsync(Domaine Domaine);
        public Task<Domaine> PutAsync(int Id, Domaine Domaine);
    }
}
