using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCoreHosted.Server.Data;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionDbsController : ControllerBase
    {
        private readonly CartographieContext _context;

        public VersionDbsController(CartographieContext context)
        {
            _context = context;
        }

        // GET: api/VersionDbs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VersionDb>>> GetVersionDbs()
        {
            var versionDb = await (from vDb in _context.VersionDbs
                                   join db in _context.Dbs on vDb.DbId equals db.Id
                                   orderby vDb.Titre descending
                                   select new VersionDb
                                   {
                                       DbId = vDb.DbId,
                                       Noyau = vDb.Noyau,
                                       Titre = vDb.Titre,
                                       VdbId = vDb.VdbId,
                                       Db = new Db
                                       {
                                           Id = db.Id,
                                           Name = db.Name
                                       }
                                   }).ToListAsync();
            return versionDb;
        }
        [HttpGet]
        [Route("getItemsByMoteurDb")]
        public async Task<ActionResult<IEnumerable<VersionDb>>> getItemsByMoteurDb(int IdDb)
        {
            var versionDb = await (from vDb in _context.VersionDbs
                                   join db in _context.Dbs on vDb.DbId equals db.Id
                                   where vDb.DbId == IdDb
                                   orderby vDb.Titre descending
                                   select new VersionDb
                                   {
                                       DbId = vDb.DbId,
                                       Noyau = vDb.Noyau,
                                       Titre = vDb.Titre,
                                       VdbId = vDb.VdbId,
                                       Db = db
                                   }).ToListAsync();
            return versionDb;
        }

        // GET: api/VersionDbs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VersionDb>> GetVersionDb(int id)
        {
            var versionDb = await (from vDb in _context.VersionDbs
                                   join db in _context.Dbs on vDb.DbId equals db.Id
                                   where vDb.VdbId == id
                                   select new VersionDb
                                   {
                                       DbId = vDb.DbId,
                                       Noyau = vDb.Noyau,
                                       Titre = vDb.Titre,
                                       VdbId = vDb.VdbId,
                                       Db = db
                                   }).FirstOrDefaultAsync();
            return versionDb;
        }

        // PUT: api/VersionDbs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVersionDb(int id, VersionDb versionDb)
        {
            if (id != versionDb.VdbId)
            {
                return BadRequest();
            }

            _context.Entry(versionDb).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VersionDbExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(versionDb);
        }

        // POST: api/VersionDbs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VersionDb>> PostVersionDb(VersionDb versionDb)
        {
            _context.VersionDbs.Add(versionDb);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVersionDb", new { id = versionDb.VdbId }, versionDb);
        }

        // DELETE: api/VersionDbs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVersionDb(int id)
        {
            var versionDb = await _context.VersionDbs.FindAsync(id);
            if (versionDb == null)
            {
                return NotFound();
            }

            _context.VersionDbs.Remove(versionDb);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VersionDbExists(int id)
        {
            return _context.VersionDbs.Any(e => e.VdbId == id);
        }
    }
}
