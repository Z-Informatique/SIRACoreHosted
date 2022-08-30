using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCoreHosted.Server.Data;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeOsController : ControllerBase
    {
        private readonly CartographieContext _context;

        public TypeOsController(CartographieContext context)
        {
            _context = context;
        }

        // GET: api/TypeOs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeO>>> GetTypeOs()
        {
            var typesOs = await (from type in _context.TypeOs
                                 orderby type.TitreOs ascending
                                 select new TypeO
                                 {
                                     TitreOs = type.TitreOs,
                                     TypeOsId = type.TypeOsId
                                 }).ToListAsync();
            return typesOs;
        }

        // GET: api/TypeOs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeO>> GetTypeO(int id)
        {
            var typeO = await (from type in _context.TypeOs
                               where type.TypeOsId == id
                               select new TypeO
                               {
                                   TitreOs = type.TitreOs,
                                   TypeOsId = type.TypeOsId
                               }).FirstOrDefaultAsync();
            return typeO;
        }

        // PUT: api/TypeOs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeO(int id, TypeO typeO)
        {
            if (id != typeO.TypeOsId)
            {
                return BadRequest();
            }

            _context.Entry(typeO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(typeO);
        }

        // POST: api/TypeOs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeO>> PostTypeO(TypeO typeO)
        {
            _context.TypeOs.Add(typeO);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTypeO", new { id = typeO.TypeOsId }, typeO);
        }

        // DELETE: api/TypeOs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeO(int id)
        {
            var typeO = await _context.TypeOs.FindAsync(id);
            if (typeO == null)
            {
                return NotFound();
            }

            _context.TypeOs.Remove(typeO);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeOExists(int id)
        {
            return _context.TypeOs.Any(e => e.TypeOsId == id);
        }
    }
}
