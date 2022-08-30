using BlazorInputFile;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace TestCoreHosted.Client.IServices
{
    public interface IUploadFile
    {
        public Task Upload(IFileListEntry file, int AppId);
    }
}
