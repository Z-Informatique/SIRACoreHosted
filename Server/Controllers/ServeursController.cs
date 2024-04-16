using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCoreHosted.Server.Data;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServeursController : ControllerBase
    {
        private readonly CartographieContext _context;

        public ServeursController(CartographieContext context)
        {
            _context = context;
        }

        // GET: api/Serveurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Serveur>>> GetServeurs()
        {
            var Serveurs = await (from serv in _context.Serveurs
                                  join env in _context.Environnements on serv.EnvId equals env.EnvId
                                  join typeOs in _context.TypeOs on serv.OsId equals typeOs.TypeOsId
                                  join Osys in _context.OSystems on serv.VersionOsId equals Osys.OId
                                  orderby serv.Nom ascending
                                  select new Serveur
                                  {
                                      ServeurId = serv.ServeurId,
                                      Nom = serv.Nom,
                                      VersionOsId = serv.VersionOsId,
                                      OsId = serv.OsId,
                                      Categorie = serv.Categorie,
                                      CoutPrice = serv.CoutPrice,
                                      EnvId = serv.EnvId,
                                      Noyau = serv.Noyau,
                                      Observation = serv.Observation,
                                      Rack = serv.Rack,
                                      Salle = serv.Salle,
                                      TypeServeur = serv.TypeServeur,
                                      VersionOs = Osys,
                                      Os = typeOs,
                                      Etat = serv.Etat,
                                      MigDate = serv.MigDate,
                                      Env = env
                                  }).ToListAsync();
            return Serveurs;
        }

        // GET: api/Serveurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Serveur>> GetServeur(int id)
        {
            var Serveur = await (from serv in _context.Serveurs
                                 join env in _context.Environnements on serv.EnvId equals env.EnvId
                                 join typeOs in _context.TypeOs on serv.OsId equals typeOs.TypeOsId
                                 join Osys in _context.OSystems on serv.VersionOsId equals Osys.OId
                                 where serv.ServeurId == id
                                 select new Serveur
                                 {
                                     ServeurId = serv.ServeurId,
                                     Nom = serv.Nom,
                                     VersionOsId = serv.VersionOsId,
                                     OsId = serv.OsId,
                                     Categorie = serv.Categorie,
                                     CoutPrice = serv.CoutPrice,
                                     EnvId = serv.EnvId,
                                     Noyau = serv.Noyau,
                                     Observation = serv.Observation,
                                     Rack = serv.Rack,
                                     Salle = serv.Salle,
                                     TypeServeur = serv.TypeServeur,
                                     VersionOs = Osys,
                                     Os = typeOs,
                                     Etat = serv.Etat,
                                     MigDate = serv.MigDate,
                                     Env = env
                                 }).FirstOrDefaultAsync();

            return Serveur;
        }

        // PUT: api/Serveurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServeur(int id, Serveur Serveur)
        {
            if (id != Serveur.ServeurId)
            {
                return BadRequest();
            }

            _context.Entry(Serveur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServeurExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(Serveur);
        }

        // POST: api/Serveurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Serveur>> PostServeur(Serveur Serveur)
        {
            _context.Serveurs.Add(Serveur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServeur", new { id = Serveur.ServeurId }, Serveur);
        }

        // DELETE: api/Serveurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServeur(int id)
        {
            var Serveur = await _context.Serveurs.FindAsync(id);
            if (Serveur == null)
            {
                return NotFound();
            }

            _context.Serveurs.Remove(Serveur);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServeurExists(int id)
        {
            return _context.Serveurs.Any(e => e.ServeurId == id);
        }
    }
}
