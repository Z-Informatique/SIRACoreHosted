using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCoreHosted.Server.Data;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Server.Controllers
{
    public class ExportFileController : ExportController
    {
        private readonly CartographieContext context;

        public ExportFileController(CartographieContext context)
        {
            this.context = context;
        }

        [HttpGet("/export/TestCoreHosted/applications/csv")]
        public FileStreamResult ExportApplicationsToCSV()
        {
            return ToCSV(ApplyQuery(context.Applications, Request.Query));
        }

        [HttpGet("/export/TestCoreHosted/applications/excel")]
        public FileStreamResult ExportApplicationsToExcel()
        {
            return ToExcel(ApplyQuery(context.Applications, Request.Query));
        }

        [HttpGet("/export/TestCoreHosted/expdatabase/csv")]
        public FileStreamResult ExportDatabasesToCSV()
        {
            List<BaseDonnees> baseDonnees = new List<BaseDonnees>();
            var DataBases = (from db in context.Databases
                                  join env in context.Environnements on db.EnvId equals env.EnvId
                                  join serv in context.Serveurs on db.ServeurId equals serv.ServeurId
                                  join vers in context.VersionDbs on db.VersionDbId equals vers.VdbId
                                  join app in context.Applications on db.AppId equals app.AppId
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
                                  }).AsQueryable();


            foreach (var data in DataBases)
            {
                baseDonnees.Add(new BaseDonnees
                {
                    Application = data.Application.Titre,
                    Noyau = data.VersionDb.Titre,
                    DataId = data.DataId,
                    DTitre = data.DTitre,
                    Environeement = data.Env.EnvType,
                    Etat = data.Etat,
                    MigDate = data.MigDate,
                    VersionDb = data.VersionDb.Noyau,
                    Serveur = data.Serveur.Nom
                });
            }


            return ToCSV(ApplyQuery(baseDonnees.AsQueryable(), Request.Query));
        }

        [HttpGet("/export/TestCoreHosted/expdatabase/excel")]
        public FileStreamResult ExportDatabasesToExcel()
        {
            List<BaseDonnees> baseDonnees = new List<BaseDonnees>();

            var DataBases = (from db in context.Databases
                             join env in context.Environnements on db.EnvId equals env.EnvId
                             join serv in context.Serveurs on db.ServeurId equals serv.ServeurId
                             join vers in context.VersionDbs on db.VersionDbId equals vers.VdbId
                             join app in context.Applications on db.AppId equals app.AppId
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
                             }).AsQueryable();

            foreach (var data in DataBases)
            {
                baseDonnees.Add(new BaseDonnees
                {
                    Application = data.Application.Titre,
                    Noyau = data.VersionDb.Titre,
                    DataId = data.DataId,
                    DTitre = data.DTitre,
                    Environeement = data.Env.EnvType,
                    Etat = data.Etat,
                    MigDate = data.MigDate,
                    VersionDb = data.VersionDb.Noyau,
                    Serveur = data.Serveur.Nom
                });
            }

            return ToExcel(ApplyQuery(baseDonnees.AsQueryable(), Request.Query));
        }

        [HttpGet("/export/TestCoreHosted/serveurs/csv")]
        public FileStreamResult ExportServeursToCSV()
        {
            return ToCSV(ApplyQuery(context.Serveurs, Request.Query));
        }

        [HttpGet("/export/TestCoreHosted/serveurs/excel")]
        public FileStreamResult ExportServeursToExcel()
        {
            return ToExcel(ApplyQuery(context.Serveurs, Request.Query));
        }

        [HttpGet("/export/TestCoreHosted/domaines/csv")]
        public FileStreamResult ExportDomainesToCSV()
        {
            return ToCSV(ApplyQuery(context.Domaines, Request.Query));
        }

        [HttpGet("/export/TestCoreHosted/domaines/excel")]
        public FileStreamResult ExportDomainesToExcel()
        {
            return ToExcel(ApplyQuery(context.Domaines, Request.Query));
        }

        [HttpGet("/export/TestCoreHosted/metiers/csv")]
        public FileStreamResult ExportMetiersEmployeesToCSV()
        {
            return ToCSV(ApplyQuery(context.Metiers, Request.Query));
        }

        [HttpGet("/export/TestCoreHosted/metiers/excel")]
        public FileStreamResult ExportMetiersToExcel()
        {
            return ToExcel(ApplyQuery(context.Metiers, Request.Query));
        }
    }
}
