using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCoreHosted.Server.Data;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetiersController : ControllerBase
    {
        private readonly CartographieContext _context;

        public MetiersController(CartographieContext context)
        {
            _context = context;
        }

        // GET: api/Metiers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Metier>>> GetMetiers()
        {
            var metiers = await (from en in _context.Metiers
                                 select new Metier
                                 {
                                     MetierId = en.MetierId,
                                     Title = en.Title
                                 }).ToListAsync();
            return metiers;
        }

        // GET: api/Metiers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Metier>> GetMetier(int id)
        {
            var metier = await (from en in _context.Metiers
                                where en.MetierId == id
                                select new Metier
                                {
                                    MetierId = en.MetierId,
                                    Title = en.Title
                                }).FirstOrDefaultAsync();

            return metier;
        }

        // PUT: api/Metiers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMetier(int id, Metier metier)
        {
            if (id != metier.MetierId)
            {
                return BadRequest();
            }

            _context.Entry(metier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MetierExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(metier);
        }

        // POST: api/Metiers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Metier>> PostMetier(Metier metier)
        {
            _context.Metiers.Add(metier);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMetier", new { id = metier.MetierId }, metier);
        }

        // DELETE: api/Metiers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMetier(int id)
        {
            var metier = await _context.Metiers.FindAsync(id);
            if (metier == null)
            {
                return NotFound();
            }

            _context.Metiers.Remove(metier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MetierExists(int id)
        {
            return _context.Metiers.Any(e => e.MetierId == id);
        }
    }
}
