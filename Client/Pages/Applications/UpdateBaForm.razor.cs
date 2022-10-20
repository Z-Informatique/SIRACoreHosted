using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Runtime.CompilerServices;
using TestCoreHosted.Client.IServices;
using TestCoreHosted.Client.Services;
using TestCoreHosted.Client.Shared;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.Pages.Applications
{
    public partial class UpdateBaForm
    {
        [CascadingParameter] MudDialogInstance MudDialogInstance { get; set; }
        private bool error { get; set; }
        public bool IsBusy { get; set; } = false;
        private string message { get; set; }
        private int IdBa { get; set; }
        private Severity Severity { get; set; }
        [Parameter] public Application Application { get; set; } = new Application();
        public Banalytic Banalytic { get; set; } = new Banalytic();
        public List<Banalytic> BAnalytics { get; set; } = new List<Banalytic>();

        private async Task Save()
        {
            if (Banalytic.Id > 0)
            {
                Application.IdBa = Banalytic.Id;
            }
            else
            {
                snackBar.Add("Veuillez choisir l'analyste métier", Severity.Error);
                return;
            }
            IsBusy = true;
            Application application = await applicationsService.PutAsync(Application.AppId, Application);
            message = "Application mise à jour avec succès.";
            Severity = Severity.Success;
            error = true;
            IsBusy = false;

            if (application != null)
            {
                snackBar.Add(message, Severity);
                MudDialogInstance.Close(DialogResult.Ok(true));
            }
            else
            {
                message = "Une erreur s'est produite lors du traitement.";
                Severity = Severity.Error;
                snackBar.Add(message, Severity);
                error = true;
            }
        }

        async Task Load()
        {
            BAnalytics = await BAnalyticService.GetAsync();
            StateHasChanged();
        }
        protected override async Task OnInitializedAsync()
        {
            await Load();
            StateHasChanged();
        }
    }
}
