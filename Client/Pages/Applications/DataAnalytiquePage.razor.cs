using Microsoft.AspNetCore.Components;
using MudBlazor;
using Radzen;
using System.Globalization;
using TestCoreHosted.Shared.Models;


namespace TestCoreHosted.Client.Pages.Applications
{
    public partial class DataAnalytiquePage
    {
        [CascadingParameter] MudDialogInstance MudDialogInstance { get; set; }
        [Parameter] public List<Application> Applications { get; set; } = new List<Application>();
        public List<Analytics> AnalyticsOnline { get; set; } = new List<Analytics>();
        public List<Analytics> AnalyticsMigre { get; set; } = new List<Analytics>();
        public List<Analytics> AnalyticsDemo { get; set; } = new List<Analytics>();
        public List<Analytics> AnalyticsMaintenance { get; set; } = new List<Analytics>();


        public List<Application> AppliOnline { get; set; } = new List<Application>();
        public List<Application> AppliMigrate { get; set; } = new List<Application>();
        public List<Application> AppliDemo { get; set; } = new List<Application>();
        public List<Application> AppliMaintenance { get; set; } = new List<Application>();


        private bool _loading { get; set; } = false;
        async Task getData()
        {
            await Task.Delay(1000);
            _loading = true;

            AppliOnline = Applications.Where(x => x.Etat == "2").ToList();
            AppliMigrate = Applications.Where(x => x.Etat == "1").ToList();
            AppliDemo = Applications.Where(x => x.Etat == "0").ToList();
            //AppliMaintenance = Applications.Where(x => x.Etat == "3").ToList();

            AnalyticsOnline = (from t in AppliOnline
                               group t by new { t.MigDate.Value.Year }
                                into grp
                               select new Analytics
                               {
                                   Year = grp.Key.Year.ToString(),
                                   Count = grp.Count()
                               }).OrderByDescending(x => x.Year).Take(5).ToList();

            AnalyticsMigre = (from t in AppliMigrate
                              group t by new { t.MigDate.Value.Year }
                              into grp
                              select new Analytics
                              {
                                  Year = grp.Key.Year.ToString(),
                                  Count = grp.Count()
                              }).OrderByDescending(x => x.Year).Take(5).ToList();

            AnalyticsDemo = (from t in AppliDemo
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

        bool showDataLabels = true;
    }
}
