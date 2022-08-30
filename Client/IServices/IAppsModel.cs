using TestCoreHosted.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestCoreHosted.Client.IServices
{
    public interface IAppsModel
    {
        public Task<List<AppsModel>> GetAppsModel();
    }
}
