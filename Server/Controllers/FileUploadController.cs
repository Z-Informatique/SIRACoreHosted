using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestCoreHosted.Server.Data;
using TestCoreHosted.Shared.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestCoreHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IHostEnvironment env;
        private readonly CartographieContext _context;

        public FileUploadController(IHostEnvironment env, CartographieContext context)
        {
            this.env = env;
            _context = context;
        }

        [HttpPost]
        [Route("save-file")]
        public async Task<IActionResult> SaveFileToLocation([FromBody] SaveFile saveFile)
        {
            try
            {
                foreach (var file in saveFile.Files)
                {
                    string FileExtension = file.FileType.ToLower();
                    string root = env.ContentRootPath;

                    var ExistPath = root + $@"Documentations\Applications\{file.Id}";

                    var fPath = $@"Documentations\Applications";
                    var defPath = env.ContentRootPath + fPath;

                    if (!Directory.Exists(ExistPath))
                    {
                        Directory.CreateDirectory(ExistPath);
                    }
                    string extension = Path.GetExtension(file.FileName);
                    string fileNameNew = $"{Guid.NewGuid()}{extension.ToLower()}";

                    string fileName = file.FileName.Replace(" ", "-").Replace("'", "-");

                    string filePath = Path.Combine(defPath, file.Id.ToString(), fileName);

                    using (var fileStream = System.IO.File.Create(filePath))
                    {
                        await fileStream.WriteAsync(file.FileContent);
                    }
                    Documentation documentation = new Documentation
                    {
                        AppId = file.Id,
                        NameFile = filePath,
                        Titre = fileName,
                        ChargerLe = DateTime.Today
                    };
                    _context.Documentations.Add(documentation);
                    await _context.SaveChangesAsync();
                }
                return Ok("Fichier enregistré avec succès.");
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
