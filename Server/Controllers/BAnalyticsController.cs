using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCoreHosted.Server.Data;
using TestCoreHosted.Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestCoreHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanalyticsController : ControllerBase
    {
        private readonly CartographieContext _context;

        public BanalyticsController(CartographieContext context)
        {
            _context = context;
        }
        // GET: api/<BanalyticsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Banalytic>>> GetBanalytics()
        {
            var Banalytics = await (from ba in _context.Banalytics
                                    orderby ba.Nom ascending
                                    select new Banalytic
                                    {
                                        Id = ba.Id,
                                        Nom = ba.Nom,
                                    }).ToListAsync();
            return Banalytics;
        }

        // GET api/<BanalyticsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Banalytic>> GetBanalytic(int id)
        {
            return await _context.Banalytics.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        // PUT: api/Banalytics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBanalytic(int id, Banalytic Banalytic)
        {
            if (id != Banalytic.Id)
            {
                return BadRequest();
            }

            _context.Entry(Banalytic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BanalyticExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(Banalytic);
        }

        // POST: api/Banalytics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Banalytic>> PostBanalytic(Banalytic Banalytic)
        {
            _context.Banalytics.Add(Banalytic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBanalytic", new { id = Banalytic.Id }, Banalytic);
        }

        // DELETE: api/Banalytics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBanalytic(int id)
        {
            var Banalytic = await _context.Banalytics.FindAsync(id);
            if (Banalytic == null)
            {
                return NotFound();
            }

            _context.Banalytics.Remove(Banalytic);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BanalyticExists(int id)
        {
            return _context.Banalytics.Any(e => e.Id == id);
        }
    }
}
