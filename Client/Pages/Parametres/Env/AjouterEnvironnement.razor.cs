using Microsoft.AspNetCore.Components;
using MudBlazor;
using TestCoreHosted.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestCoreHosted.Client.Pages.Parametres.Env
{
    public partial class AjouterEnvironnement
    {
        [CascadingParameter] MudDialogInstance MudDialogInstance { get; set; }
        private bool error { get; set; }
        public bool IsBusy { get; set; } = false;
        private string message { get; set; }
        private Severity Severity { get; set; }
        [Parameter] public Environnement Environnement { get; set; } = new Environnement();
        private async Task Save()
        {
            Environnement Env;
            if (Environnement.EnvId > 0)
            {
                Env = await envService.PutAsync(Environnement.EnvId, Environnement);
                message = "Environnement mis à jour avec succès.";
                Severity = Severity.Success;
                error = true;
            }
            else
            {
                Env = await envService.PostAsync(Environnement);
                message = "Environnement enregistré avec succès.";
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
