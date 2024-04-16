using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCoreHosted.Server.Data;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataBasesController : ControllerBase
    {
        private readonly CartographieContext _context;

        public DataBasesController(CartographieContext context)
        {
            _context = context;
        }

        // GET: api/DataBases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataBase>>> GetDatabases()
        {
            var DataBases = await (from db in _context.Databases
                                   join env in _context.Environnements on db.EnvId equals env.EnvId
                                   join serv in _context.Serveurs on db.ServeurId equals serv.ServeurId
                                   join vers in _context.VersionDbs on db.VersionDbId equals vers.VdbId
                                   join app in _context.Applications on db.AppId equals app.AppId
                                   orderby db.DTitre ascending
                                   select new DataBase
                                   {
                                       AppId = db.AppId,
                                       DTitre = db.DTitre,
                                       VersionDbId = db.VersionDbId,
                                       ServeurId = db.ServeurId,
                                       EnvId = db.EnvId,
                                       CoutPrice = db.CoutPrice,
                                       DataId = db.DataId,
                                       Etat = db.Etat,
                                       MigDate = db.MigDate,
                                       Env = new Environnement
                                       {
                                           EnvId = env.EnvId,
                                           EnvType = env.EnvType,
                                       },
                                       Serveur = new Serveur
                                       {
                                           ServeurId = serv.ServeurId,
                                           Nom = serv.Nom,
                                           MigDate = serv.MigDate,
                                           Etat = serv.Etat,
                                           Salle = serv.Salle,
                                           Rack = serv.Rack,
                                           OsId = serv.OsId,
                                           EnvId = serv.EnvId,
                                           Categorie = serv.Categorie,
                                           TypeServeur = serv.TypeServeur,
                                           Noyau = serv.Noyau,
                                           VersionOsId = serv.VersionOsId,
                                           CoutPrice = serv.CoutPrice,
                                           Observation = serv.Observation
                                       },
                                       VersionDb = new VersionDb
                                       {
                                           VdbId = vers.VdbId,
                                           Titre = vers.Titre,
                                           Noyau = vers.Noyau,
                                           DbId = vers.DbId
                                       },
                                       Application = new Application
                                       {
                                           AppId = app.AppId,
                                           Titre = app.Titre,
                                           Architecture = app.Architecture,
                                           Ba = app.Ba,
                                           Bm = app.Bm,
                                           Commentaire = app.Commentaire,
                                           Dependences = app.Dependences,
                                           Conformite = app.Conformite,
                                           MigDate = app.MigDate,
                                           ServeurId = app.ServeurId,
                                           VersionApp = app.VersionApp,
                                           SiteApp = app.SiteApp,
                                           Cout = app.Cout,
                                           Etat = app.Etat,
                                           Escalade = app.Escalade,
                                           Impact = app.Impact,
                                           ContactUser = app.ContactUser,
                                           NbreUser = app.NbreUser,
                                           Description = app.Description
                                       }
                                   }).ToListAsync();
            return DataBases;
        }

        // GET: api/DataBases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataBase>> GetDataBase(int id)
        {
            var DataBase = await (from db in _context.Databases
                                  join env in _context.Environnements on db.EnvId equals env.EnvId
                                  join serv in _context.Serveurs on db.ServeurId equals serv.ServeurId
                                  join vers in _context.VersionDbs on db.VersionDbId equals vers.VdbId
                                  join app in _context.Applications on db.AppId equals app.AppId
                                  where db.DataId == id
                                  select new DataBase
                                  {
                                      AppId = db.AppId,
                                      DTitre = db.DTitre,
                                      VersionDbId = db.VersionDbId,
                                      ServeurId = db.ServeurId,
                                      EnvId = db.EnvId,
                                      CoutPrice = db.CoutPrice,
                                      DataId = db.DataId,
                                      Etat = db.Etat,
                                      MigDate = db.MigDate,
                                      Env = new Environnement
                                      {
                                          EnvId = env.EnvId,
                                          EnvType = env.EnvType,
                                      },
                                      Serveur = new Serveur
                                      {
                                          ServeurId = serv.ServeurId,
                                          Nom = serv.Nom,
                                          MigDate = serv.MigDate,
                                          Etat = serv.Etat,
                                          Salle = serv.Salle,
                                          Rack = serv.Rack,
                                          OsId = serv.OsId,
                                          EnvId = serv.EnvId,
                                          Categorie = serv.Categorie,
                                          TypeServeur = serv.TypeServeur,
                                          Noyau = serv.Noyau,
                                          VersionOsId = serv.VersionOsId,
                                          CoutPrice = serv.CoutPrice,
                                          Observation = serv.Observation
                                      },
                                      VersionDb = new VersionDb
                                      {
                                          VdbId = vers.VdbId,
                                          Titre = vers.Titre,
                                          Noyau = vers.Noyau,
                                          DbId = vers.DbId
                                      },
                                      Application = new Application
                                      {
                                          AppId = app.AppId,
                                          Titre = app.Titre,
                                          Architecture = app.Architecture,
                                          Ba = app.Ba,
                                          Bm = app.Bm,
                                          Commentaire = app.Commentaire,
                                          Dependences = app.Dependences,
                                          Conformite = app.Conformite,
                                          MigDate = app.MigDate,
                                          ServeurId = app.ServeurId,
                                          VersionApp = app.VersionApp,
                                          SiteApp = app.SiteApp,
                                          Cout = app.Cout,
                                          Etat = app.Etat,
                                          Escalade = app.Escalade,
                                          Impact = app.Impact,
                                          ContactUser = app.ContactUser,
                                          NbreUser = app.NbreUser,
                                          Description = app.Description
                                      }
                                  }).FirstOrDefaultAsync();

            return DataBase;
        }

        // PUT: api/DataBases/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDataBase(int id, DataBase dataBase)
        {
            if (id != dataBase.DataId)
            {
                return BadRequest();
            }

            _context.Entry(dataBase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DataBaseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(dataBase);
        }

        // POST: api/DataBases
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DataBase>> PostDataBase(DataBase dataBase)
        {
            _context.Databases.Add(dataBase);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDataBase", new { id = dataBase.DataId }, dataBase);
        }

        // DELETE: api/DataBases/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDataBase(int id)
        {
            var dataBase = await _context.Databases.FindAsync(id);
            if (dataBase == null)
            {
                return NotFound();
            }

            _context.Databases.Remove(dataBase);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DataBaseExists(int id)
        {
            return _context.Databases.Any(e => e.DataId == id);
        }
    }
}
