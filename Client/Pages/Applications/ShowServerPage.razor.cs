using Microsoft.AspNetCore.Components;
using MudBlazor;
using Radzen.Blazor;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.Pages.Applications
{
    public partial class ShowServerPage
    {
        [CascadingParameter] MudDialogInstance MudDialogInstance { get; set; }
        [Parameter] public Application Application { get; set; } = new Application();
        RadzenDataGrid<Serveur> grid;
        public List<Serveur> Serveurs { get; set; } = new List<Serveur>();
        private bool _loading { get; set; } = false;
        async Task getData()
        {
            Serveurs.Clear();
            _loading = true;
            if (!string.IsNullOrEmpty(Application.ServeurId))
            {
                List<string> results = Application.ServeurId.Split(',').ToList();
                if (results.Count > 0)
                {
                    foreach (var rs in results)
                    {
                        var result = await serveurService.GetAsyncItem(Convert.ToInt32(rs));
                        if (result != null)
                        {
                            Serveurs.Add(result);
                        }
                    }
                }
            }
            _loading = false;
            StateHasChanged();
        }
        protected override async Task OnInitializedAsync()
        {
            await getData();
            StateHasChanged();
        }
    }
}
