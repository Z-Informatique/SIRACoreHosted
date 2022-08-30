﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.Pages.BaseDeDonnees
{
    public partial class DataAnalytiquePage
    {
        [CascadingParameter] MudDialogInstance MudDialogInstance { get; set; }
        [Parameter] public List<DataBase> DataBases { get; set; } = new List<DataBase>();
        public List<Analytics> AnalyticsOnline { get; set; } = new List<Analytics>();
        public List<Analytics> AnalyticsMigre { get; set; } = new List<Analytics>();
        public List<Analytics> AnalyticsDemo { get; set; } = new List<Analytics>();
        public List<Analytics> AnalyticsMaintenance { get; set; } = new List<Analytics>();
        private bool _loading { get; set; } = false;
        async Task getData()
        {
            await Task.Delay(1000);
            _loading = true;

            var servOnline = DataBases.Where(x => x.Etat == "3").ToList().Take(5);
            var servMigre = DataBases.Where(x => x.Etat == "2").ToList().Take(5);
            var servDemo = DataBases.Where(x => x.Etat == "1").ToList().Take(5);
            var servMaintenance = DataBases.Where(x => x.Etat == "0").ToList().Take(5);

            AnalyticsOnline = (from t in servOnline
                               group t by new { t.MigDate.Value.Year }
                                into grp
                               select new Analytics
                               {
                                   Year = grp.Key.Year,
                                   Count = grp.Count()
                               }).ToList();

            AnalyticsMigre = (from t in servMigre
                              group t by new { t.MigDate.Value.Year }
                              into grp
                              select new Analytics
                              {
                                  Year = grp.Key.Year,
                                  Count = grp.Count()
                              }).ToList();

            AnalyticsDemo = (from t in servDemo
                             group t by new { t.MigDate.Value.Year }
                             into grp
                             select new Analytics
                             {
                                 Year = grp.Key.Year,
                                 Count = grp.Count()
                             }).ToList();

            AnalyticsMaintenance = (from t in servMaintenance
                                    group t by new { t.MigDate.Value.Year }
                                    into grp
                                    select new Analytics
                                    {
                                        Year = grp.Key.Year,
                                        Count = grp.Count()
                                    }).ToList();
            _loading = false;
        }
        protected override async Task OnInitializedAsync()
        {
            await getData();
            StateHasChanged();
        }
    }
}