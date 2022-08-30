using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.IServices
{
    public interface IServeur
    {
        public Task<List<Serveur>> GetAsync();
        public Task<Serveur> GetAsyncItem(int id);
        public Task<Serveur> VerifDataByString(string data);
        public Task<bool> Delete(int id);
        public Task<Serveur> PostAsync(Serveur Serveur);
        public Task<Serveur> PutAsync(int Id, Serveur Serveur);
    }
}
