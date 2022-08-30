using BlazorInputFile;
using TestCoreHosted.Client.IServices;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace TestCoreHosted.Client.Services
{
    public class UploadFile : IUploadFile
    {
        private readonly IHostingEnvironment webHostEnvironment;
        public UploadFile(IHostingEnvironment hostingEnvironment)
        {
            webHostEnvironment.EnvironmentName = "Development";
            webHostEnvironment = hostingEnvironment;
        }
        public async Task Upload(IFileListEntry file, int AppId)
        {
            var path = Path.Combine(webHostEnvironment.ContentRootPath, $"Documentations", file.Name);
            var memoryStream = new MemoryStream();

            await file.Data.CopyToAsync(memoryStream);

            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                memoryStream.WriteTo(fileStream);
            }
        }
    }
}
