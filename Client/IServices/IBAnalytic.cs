using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.IServices
{
    public interface IBAnalytic
    {
        public Task<List<Banalytic>> GetAsync();
        public Task<Banalytic> GetAsyncItem(int id);
        public Task<Banalytic> VerifDataByString(string data);
        public Task<bool> Delete(int id);
        public Task<Banalytic> PostAsync(Banalytic BAnalytic);
        public Task<Banalytic> PutAsync(int Id, Banalytic BAnalytic);
    }
}
