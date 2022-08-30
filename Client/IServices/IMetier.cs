using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.IServices
{
    public interface IMetier
    {
        public Task<List<Metier>> GetAsync();
        public Task<Metier> GetAsyncItem(int id);
        public Task<Metier> VerifDataByString(string data);
        public Task<bool> Delete(int id);
        public Task<Metier> PostAsync(Metier Metier);
        public Task<Metier> PutAsync(int Id, Metier Metier);
    }
}
