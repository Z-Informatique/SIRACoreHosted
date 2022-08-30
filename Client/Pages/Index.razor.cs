using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.Pages
{
    public partial class Index
    {
        public List<Analytics> analyticsApps { get; set; } = new List<Analytics>();
        public List<Analytics> analyticsDomaine { get; set; } = new List<Analytics>();
        public List<Application> applications { get; set; } = new List<Application>();
        private int AppSiege { get; set; }
        private int AppLocal { get; set; }
        async Task LoadChartApp()
        {
            analyticsApps.Clear();

            AppSiege = await applicationService.CountDataByLocation("Siège");
            AppLocal = await applicationService.CountDataByLocation("Local");

            analyticsApps = new List<Analytics>
            {
                new Analytics
                {
                    Etat = "Consolidées",
                    Count = AppSiege,
                },
                new Analytics
                {
                    Etat = "Local",
                    Count = AppLocal
                }
            };

            applications = await applicationService.GetAsync();

            analyticsDomaine = (from t in applications
                                group t by new { t.Domaine.DTitle }
                                into grp
                                select new Analytics
                                {
                                    Etat = grp.Key.DTitle,
                                    Count = grp.Count()
                                }).ToList();


            StateHasChanged();
        }
        protected override async Task OnInitializedAsync()
        {
            await LoadChartApp();
            StateHasChanged();
        }
    }
}
