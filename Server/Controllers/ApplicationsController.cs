using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCoreHosted.Server.Data;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly CartographieContext _context;

        public ApplicationsController(CartographieContext context)
        {
            _context = context;
        }

        // GET: api/Applications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> GetApplications()
        {
            var applications = await (from apps in _context.Applications
                                      join metier in _context.Metiers on apps.MetierId equals metier.MetierId
                                      join domaine in _context.Domaines on apps.DomaineId equals domaine.DomaineId
                                      join ba in _context.Banalytics on apps.IdBa equals ba.Id
                                      orderby apps.Titre ascending
                                      select new Application
                                      {
                                          AppId = apps.AppId,
                                          Titre = apps.Titre,
                                          Architecture = apps.Architecture,
                                          Ba = apps.Ba,
                                          Bm = apps.Bm,
                                          Commentaire = apps.Commentaire,
                                          DemoDate = apps.DemoDate,
                                          Conformite = apps.Conformite,
                                          ContactUser = apps.ContactUser,
                                          Escalade = apps.Escalade,
                                          Perimetre = apps.Perimetre,
                                          Dependences = apps.Dependences,
                                          Cout = apps.Cout,
                                          CoutPrice = apps.CoutPrice,
                                          Editeur = apps.Editeur,
                                          MigDate = apps.MigDate,
                                          Etat = apps.Etat,
                                          Documentation = apps.Documentation,
                                          Impact = apps.Impact,
                                          NbreUser = apps.NbreUser,
                                          VersionApp = apps.VersionApp,
                                          SiteApp = apps.SiteApp,
                                          Description = apps.Description,
                                          ServeurId = apps.ServeurId,
                                          MetierId = apps.MetierId,
                                          Metier = new Metier
                                          {
                                              MetierId = metier.MetierId,
                                              Title = metier.Title
                                          },
                                          DomaineId = apps.DomaineId,
                                          Domaine = new Domaine
                                          {
                                              DTitle = domaine.DTitle,
                                              DomaineId = domaine.DomaineId,
                                          },
                                          Banalytic = new Banalytic
                                          {
                                              Id = ba.Id,
                                              Nom = ba.Nom
                                          },
                                          OnlineDate = apps.OnlineDate
                                      }).ToListAsync();
            return applications;
        }

        [HttpGet]
        [Route("getListe")]
        public async Task<ActionResult<IEnumerable<Application>>> getListe()
        {
            return await _context.Applications.ToListAsync();
        }

        [HttpGet]
        [Route("CountAppsByLocation")]
        public async Task<int> CountAppsByLocation(string location)
        {
            return await _context.Applications.Where(x => x.SiteApp == location).CountAsync();
        }


        [HttpGet]
        [Route("GetAppsModel")]
        public async Task<ActionResult<IEnumerable<AppsModel>>> GetAppsModel()
        {
            List<string> ServeurName = new List<string>();
            List<AppsModel> appl = new List<AppsModel>();

            var applications = await (from apps in _context.Applications
                                      join metier in _context.Metiers on apps.MetierId equals metier.MetierId
                                      join domaine in _context.Domaines on apps.DomaineId equals domaine.DomaineId
                                      orderby apps.Titre ascending
                                      select new Application
                                      {
                                          AppId = apps.AppId,
                                          Titre = apps.Titre,
                                          Architecture = apps.Architecture,
                                          Ba = apps.Ba,
                                          Bm = apps.Bm,
                                          Commentaire = apps.Commentaire,
                                          DemoDate = apps.DemoDate,
                                          Conformite = apps.Conformite,
                                          ContactUser = apps.ContactUser,
                                          Escalade = apps.Escalade,
                                          Perimetre = apps.Perimetre,
                                          Dependences = apps.Dependences,
                                          Cout = apps.Cout,
                                          CoutPrice = apps.CoutPrice,
                                          Editeur = apps.Editeur,
                                          MigDate = apps.MigDate,
                                          Etat = apps.Etat,
                                          Documentation = apps.Documentation,
                                          Impact = apps.Impact,
                                          NbreUser = apps.NbreUser,
                                          VersionApp = apps.VersionApp,
                                          SiteApp = apps.SiteApp,
                                          Description = apps.Description,
                                          ServeurId = apps.ServeurId,
                                          MetierId = apps.MetierId,
                                          Metier = new Metier
                                          {
                                              MetierId = metier.MetierId,
                                              Title = metier.Title
                                          },
                                          DomaineId = apps.DomaineId,
                                          Domaine = new Domaine
                                          {
                                              DTitle = domaine.DTitle,
                                              DomaineId = domaine.DomaineId,
                                          },
                                          OnlineDate = apps.OnlineDate
                                      }).ToListAsync();

            AppsModel ap;
            foreach (var apps in applications)
            {
                ap = new AppsModel
                {
                    AppId = apps.AppId,
                    Titre = apps.Titre,
                    Architecture = apps.Architecture,
                    Ba = apps.Ba,
                    Bm = apps.Bm,
                    Commentaire = apps.Commentaire,
                    DemoDate = apps.DemoDate,
                    Conformite = apps.Conformite,
                    ContactUser = apps.ContactUser,
                    Escalade = apps.Escalade,
                    Perimetre = apps.Perimetre,
                    Dependences = apps.Dependences,
                    Cout = apps.Cout,
                    CoutPrice = apps.CoutPrice,
                    Editeur = apps.Editeur,
                    MigDate = apps.MigDate,
                    Etat = apps.Etat,
                    Documentation = apps.Documentation,
                    Impact = apps.Impact,
                    NbreUser = apps.NbreUser,
                    VersionApp = apps.VersionApp,
                    SiteApp = apps.SiteApp,
                    Description = apps.Description,
                    ServeurId = apps.ServeurId,
                    MetierId = apps.MetierId,
                    Metier = apps.Metier,
                    DomaineId = apps.DomaineId,
                    Domaine = apps.Domaine,
                    OnlineDate = apps.OnlineDate

                };
                appl.Add(ap);
            }

            foreach (var item in appl)
            {
                ServeurName.Clear();
                if (!string.IsNullOrEmpty(item.ServeurId))
                {
                    List<string> results = item.ServeurId.Split(',').ToList();
                    if (results.Count > 0)
                    {
                        foreach (var rs in results)
                        {
                            var result = await _context.Serveurs.Where(x => x.ServeurId == Convert.ToInt32(rs)).FirstOrDefaultAsync();
                            if (result != null)
                            {
                                ServeurName.Add(result.Nom);
                            }
                        }
                    }
                }
                item.ServeurList = String.Join(",", ServeurName);
            }

            return appl;
        }

        // GET: api/Applications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Application>> GetApplication(int id)
        {
            var application = await (from apps in _context.Applications
                                     join metier in _context.Metiers on apps.MetierId equals metier.MetierId
                                     join domaine in _context.Domaines on apps.DomaineId equals domaine.DomaineId
                                     join ba in _context.Banalytics on apps.IdBa equals ba.Id
                                     where apps.AppId == id
                                     select new Application
                                     {
                                         AppId = apps.AppId,
                                         Titre = apps.Titre,
                                         Architecture = apps.Architecture,
                                         Ba = apps.Ba,
                                         Bm = apps.Bm,
                                         Commentaire = apps.Commentaire,
                                         DemoDate = apps.DemoDate,
                                         Conformite = apps.Conformite,
                                         ContactUser = apps.ContactUser,
                                         Escalade = apps.Escalade,
                                         Perimetre = apps.Perimetre,
                                         Dependences = apps.Dependences,
                                         Cout = apps.Cout,
                                         CoutPrice = apps.CoutPrice,
                                         Editeur = apps.Editeur,
                                         MigDate = apps.MigDate,
                                         Etat = apps.Etat,
                                         Documentation = apps.Documentation,
                                         Impact = apps.Impact,
                                         NbreUser = apps.NbreUser,
                                         VersionApp = apps.VersionApp,
                                         SiteApp = apps.SiteApp,
                                         Description = apps.Description,
                                         ServeurId = apps.ServeurId,
                                         MetierId = apps.MetierId,
                                         Metier = new Metier
                                         {
                                             MetierId = metier.MetierId,
                                             Title = metier.Title
                                         },
                                         DomaineId = apps.DomaineId,
                                         Domaine = new Domaine
                                         {
                                             DTitle = domaine.DTitle,
                                             DomaineId = domaine.DomaineId,
                                         },
                                         Banalytic = new Banalytic
                                         {
                                             Id = ba.Id,
                                             Nom = ba.Nom
                                         },
                                         OnlineDate = apps.OnlineDate
                                     }).FirstOrDefaultAsync();

            return application;
        }

        // PUT: api/Applications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication(int id, Application application)
        {
            if (id != application.AppId)
            {
                return BadRequest();
            }

            _context.Entry(application).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(application);
        }

        // POST: api/Applications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Application>> PostApplication(Application application)
        {
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplication", new { id = application.AppId }, application);
        }

        // DELETE: api/Applications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.AppId == id);
        }
    }
}
