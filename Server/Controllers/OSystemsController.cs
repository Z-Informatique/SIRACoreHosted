using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCoreHosted.Server.Data;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OSystemsController : ControllerBase
    {
        private readonly CartographieContext _context;

        public OSystemsController(CartographieContext context)
        {
            _context = context;
        }

        // GET: api/OSystems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OSystem>>> GetOSystems()
        {
            var oSystems = await (from os in _context.OSystems
                                  join type in _context.TypeOs on os.Title equals type.TypeOsId
                                  orderby os.VersionO ascending
                                  select new OSystem
                                  {
                                      VersionO = os.VersionO,
                                      OId = os.OId,
                                      Title = os.Title,
                                      TypeO = type
                                  }).ToListAsync();
            return oSystems;
        }

        // GET: api/OSystems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OSystem>> GetOSystem(int id)
        {
            var oSystem = await (from os in _context.OSystems
                                 join type in _context.TypeOs on os.Title equals type.TypeOsId
                                 where os.OId == id
                                 select new OSystem
                                 {
                                     VersionO = os.VersionO,
                                     OId = os.OId,
                                     Title = os.Title,
                                     TypeO = type
                                 }).FirstOrDefaultAsync();
            return oSystem;
        }

        // GET: api/OSystems/5
        [HttpGet]
        [Route("GetAsyncItemByIdTypeOs")]
        public async Task<ActionResult<IEnumerable<OSystem>>> GetAsyncItemByIdTypeOs([FromQuery] int IdTypeOs)
        {
            var oSystems = await (from os in _context.OSystems
                                  join type in _context.TypeOs on os.Title equals type.TypeOsId
                                  where os.Title == IdTypeOs
                                  select new OSystem
                                  {
                                      VersionO = os.VersionO,
                                      OId = os.OId,
                                      Title = os.Title,
                                      TypeO = type
                                  }).ToListAsync();
            return oSystems;
        }

        // PUT: api/OSystems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOSystem(int id, OSystem oSystem)
        {
            if (id != oSystem.OId)
            {
                return BadRequest();
            }

            _context.Entry(oSystem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OSystemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(oSystem);
        }

        // POST: api/OSystems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OSystem>> PostOSystem(OSystem oSystem)
        {
            _context.OSystems.Add(oSystem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOSystem", new { id = oSystem.OId }, oSystem);
        }

        // DELETE: api/OSystems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOSystem(int id)
        {
            var oSystem = await _context.OSystems.FindAsync(id);
            if (oSystem == null)
            {
                return NotFound();
            }

            _context.OSystems.Remove(oSystem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OSystemExists(int id)
        {
            return _context.OSystems.Any(e => e.OId == id);
        }
    }
}
