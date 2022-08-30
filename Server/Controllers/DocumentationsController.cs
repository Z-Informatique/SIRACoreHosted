using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCoreHosted.Server.Data;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentationsController : ControllerBase
    {
        private readonly CartographieContext _context;
        private readonly IHostEnvironment env;

        public DocumentationsController(IHostEnvironment env, CartographieContext context)
        {
            _context = context;
            this.env = env;
        }

        // GET: api/Documentations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Documentation>>> GetDocumentations()
        {
            var documentations = await (from doc in _context.Documentations
                                        join app in _context.Applications on doc.AppId equals app.AppId
                                        orderby doc.NameFile ascending
                                        select new Documentation
                                        {
                                            AppId = doc.AppId,
                                            ChargerLe = doc.ChargerLe,
                                            Id = doc.Id,
                                            NameFile = doc.NameFile,
                                            Titre = doc.Titre,
                                            App = app
                                        }).ToListAsync();
            return documentations;
        }

        [HttpGet]
        [Route("GetAsyncByAppId")]
        public async Task<ActionResult<IEnumerable<Documentation>>> GetAsyncByAppId(int IdApp)
        {
            var documentations = await (from doc in _context.Documentations
                                        join app in _context.Applications on doc.AppId equals app.AppId
                                        where doc.AppId == IdApp
                                        orderby doc.NameFile ascending
                                        select new Documentation
                                        {
                                            AppId = doc.AppId,
                                            ChargerLe = doc.ChargerLe,
                                            Id = doc.Id,
                                            NameFile = doc.NameFile,
                                            Titre = doc.Titre,
                                        }).ToListAsync();
            return documentations;
        }

        // GET: api/Documentations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Documentation>> GetDocumentation(int id)
        {
            var documentation = await (from doc in _context.Documentations
                                       join app in _context.Applications on doc.AppId equals app.AppId
                                       where doc.Id == id
                                       select new Documentation
                                       {
                                           AppId = doc.AppId,
                                           ChargerLe = doc.ChargerLe,
                                           Id = doc.Id,
                                           NameFile = doc.NameFile,
                                           Titre = doc.Titre,
                                           App = app
                                       }).FirstOrDefaultAsync();

            return documentation;
        }

        // PUT: api/Documentations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumentation(int id, Documentation documentation)
        {
            if (id != documentation.Id)
            {
                return BadRequest();
            }

            _context.Entry(documentation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(documentation);
        }

        // POST: api/Documentations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Documentation>> PostDocumentation(Documentation documentation)
        {
            _context.Documentations.Add(documentation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDocumentation", new { id = documentation.Id }, documentation);
        }

        // DELETE: api/Documentations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentation(int id)
        {
            var documentation = await _context.Documentations.FindAsync(id);
            if (documentation == null)
            {
                return NotFound();
            }

            _context.Documentations.Remove(documentation);
            await _context.SaveChangesAsync();

            var ExistPath = env.ContentRootPath + $@"Documentations\Applications\{documentation.AppId}\{documentation.Titre}";

            if (System.IO.File.Exists(documentation.NameFile))
            {
                System.IO.File.Delete(documentation.NameFile);
            }

            return NoContent();
        }

        private bool DocumentationExists(int id)
        {
            return _context.Documentations.Any(e => e.Id == id);
        }
    }
}
