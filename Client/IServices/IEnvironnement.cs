using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.IServices
{
    public interface IEnvironnement
    {
        public Task<List<Environnement>> GetAsync();
        public Task<Environnement> GetAsyncItem(int id);
        public Task<Environnement> VerifDataByString(string data);
        public Task<bool> Delete(int id);
        public Task<Environnement> PostAsync(Environnement Environnement);
        public Task<Environnement> PutAsync(int Id, Environnement Environnement);
    }
}
