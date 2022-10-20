using Microsoft.AspNetCore.Components;
using MudBlazor;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.Pages.Serveurs
{
    public partial class DataAnalytiquePage
    {
        [CascadingParameter] MudDialogInstance MudDialogInstance { get; set; }
        [Parameter] public List<Serveur> Serveurs { get; set; } = new List<Serveur>();
        public List<Analytics> AnalyticsOnline { get; set; } = new List<Analytics>();
        public List<Analytics> AnalyticsMigre { get; set; } = new List<Analytics>();
        public List<Analytics> AnalyticsDemo { get; set; } = new List<Analytics>();
        public List<Analytics> AnalyticsMaintenance { get; set; } = new List<Analytics>();
        private bool _loading { get; set; } = false;
        async Task getData()
        {
            await Task.Delay(1000);
            _loading = true;

            var servOnline = Serveurs.Where(x => x.Etat == "3").ToList();
            var servMigre = Serveurs.Where(x => x.Etat == "2").ToList();
            var servDemo = Serveurs.Where(x => x.Etat == "1").ToList();
            var servMaintenance = Serveurs.Where(x => x.Etat == "0").ToList();

            AnalyticsOnline = (from t in servOnline
                               group t by new { t.MigDate.Value.Year }
                                into grp
                               select new Analytics
                               {
                                   Year = grp.Key.Year.ToString(),
                                   Count = grp.Count()
                               }).OrderByDescending(x => x.Year).Take(5).ToList();

            AnalyticsMigre = (from t in servMigre
                              group t by new { t.MigDate.Value.Year }
                              into grp
                              select new Analytics
                              {
                                  Year = grp.Key.Year.ToString(),
                                  Count = grp.Count()
                              }).OrderByDescending(x => x.Year).Take(5).ToList();

            AnalyticsDemo = (from t in servDemo
                             group t by new { t.MigDate.Value.Year }
                             into grp
                             select new Analytics
                             {
                                 Year = grp.Key.Year.ToString(),
                                 Count = grp.Count()
                             }).OrderByDescending(x => x.Year).Take(5).ToList();

            AnalyticsMaintenance = (from t in servMaintenance
                                    group t by new { t.MigDate.Value.Year }
                                    into grp
                                    select new Analytics
                                    {
                                        Year = grp.Key.Year.ToString(),
                                        Count = grp.Count()
                                    }).OrderByDescending(x => x.Year).Take(5).ToList();
            _loading = false;
        }
        protected override async Task OnInitializedAsync()
        {
            await getData();
            StateHasChanged();
        }
    }
}
