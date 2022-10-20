using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.IServices
{
    public interface IUsers
    {
        public Task<List<User>> GetAsync();
        public Task<List<User>> getUsersByRole(int IntRole);
        public Task<User> GetAsyncItem(int id);
        public Task<User> VerifDataByString(string email);
        public Task<User> Login(string email, string password);
        public Task<bool> Delete(int id);
        public Task<User> PostAsync(User user);
        public Task<User> PutAsync(int Id, User user);
        public Task<User> ChangePassword(string email, string currentPassword, string password);
    }
}
