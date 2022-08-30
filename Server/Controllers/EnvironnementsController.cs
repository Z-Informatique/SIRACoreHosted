using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCoreHosted.Server.Data;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironnementsController : ControllerBase
    {
        private readonly CartographieContext _context;

        public EnvironnementsController(CartographieContext context)
        {
            _context = context;
        }

        // GET: api/Environnements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Environnement>>> GetEnvironnements()
        {
            var envs = await (from en in _context.Environnements
                              select new Environnement
                              {
                                  EnvId = en.EnvId,
                                  EnvType = en.EnvType
                              }).ToListAsync();
            return envs;
        }

        // GET: api/Environnements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Environnement>> GetEnvironnement(int id)
        {
            var environnement = await (from en in _context.Environnements
                                       where en.EnvId == id
                                       select new Environnement
                                       {
                                           EnvId = en.EnvId,
                                           EnvType = en.EnvType
                                       }).FirstOrDefaultAsync();
            return environnement;
        }

        // PUT: api/Environnements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnvironnement(int id, Environnement environnement)
        {
            if (id != environnement.EnvId)
            {
                return BadRequest();
            }

            _context.Entry(environnement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnvironnementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(environnement);
        }

        // POST: api/Environnements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Environnement>> PostEnvironnement(Environnement environnement)
        {
            _context.Environnements.Add(environnement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnvironnement", new { id = environnement.EnvId }, environnement);
        }

        // DELETE: api/Environnements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnvironnement(int id)
        {
            var environnement = await _context.Environnements.FindAsync(id);
            if (environnement == null)
            {
                return NotFound();
            }

            _context.Environnements.Remove(environnement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnvironnementExists(int id)
        {
            return _context.Environnements.Any(e => e.EnvId == id);
        }
    }
}
