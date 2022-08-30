using Microsoft.AspNetCore.Mvc;
using TestCoreHosted.Server.Data;

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

        [HttpGet("/export/TestCoreHosted/databases/csv")]
        public FileStreamResult ExportDatabasesToCSV()
        {
            return ToCSV(ApplyQuery(context.Databases, Request.Query));
        }

        [HttpGet("/export/TestCoreHosted/databases/excel")]
        public FileStreamResult ExportDatabasesToExcel()
        {
            return ToExcel(ApplyQuery(context.Databases, Request.Query));
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
