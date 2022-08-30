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
    public class DbsController : ControllerBase
    {
        private readonly CartographieContext _context;

        public DbsController(CartographieContext context)
        {
            _context = context;
        }

        // GET: api/Dbs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Db>>> GetDbs()
        {
            var dbs = await (from db in _context.Dbs
                             select new Db
                             {
                                 Id = db.Id,
                                 Name = db.Name,
                             }).ToListAsync();
            return dbs;
        }

        // GET: api/Dbs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Db>> GetDb(int id)
        {
            var db = await _context.Dbs.FindAsync(id);

            if (db == null)
            {
                return NotFound();
            }

            return db;
        }

        // PUT: api/Dbs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDb(int id, Db db)
        {
            if (id != db.Id)
            {
                return BadRequest();
            }

            _context.Entry(db).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DbExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(db);
        }

        // POST: api/Dbs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Db>> PostDb(Db db)
        {
            _context.Dbs.Add(db);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDb", new { id = db.Id }, db);
        }

        // DELETE: api/Dbs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDb(int id)
        {
            var db = await _context.Dbs.FindAsync(id);
            if (db == null)
            {
                return NotFound();
            }

            _context.Dbs.Remove(db);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DbExists(int id)
        {
            return _context.Dbs.Any(e => e.Id == id);
        }
    }
}
