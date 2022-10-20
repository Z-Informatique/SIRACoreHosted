using Microsoft.AspNetCore.Components;
using MudBlazor;
using TestCoreHosted.Client.Services;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.Pages.Parametres.Ba
{
    public partial class AjouterBAnalytic
    {
        [CascadingParameter] MudDialogInstance MudDialogInstance { get; set; }
        private bool error { get; set; }
        public bool IsBusy { get; set; } = false;
        private string message { get; set; }
        private Severity Severity { get; set; }
        [Parameter] public Banalytic BAnalytic { get; set; } = new Banalytic();
        private async Task Save()
        {
            Banalytic Env;
            if (BAnalytic.Id > 0)
            {
                Env = await BAnalyticService.PutAsync(BAnalytic.Id, BAnalytic);
                message = "Données mis à jour avec succès.";
                Severity = Severity.Success;
                error = true;
            }
            else
            {
                Env = await BAnalyticService.PostAsync(BAnalytic);
                message = "Données enregistré avec succès.";
                Severity = Severity.Success;
                error = true;
            }

            if (Env != null)
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
    }
}
