using Microsoft.AspNetCore.Components;
using MudBlazor;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.Pages.Parametres.Domaines
{
    public partial class AjouterDomaine
    {
        [CascadingParameter] MudDialogInstance MudDialogInstance { get; set; }
        private bool error { get; set; }
        public bool IsBusy { get; set; } = false;
        private string message { get; set; }
        private Severity Severity { get; set; }
        [Parameter] public Domaine Domaine { get; set; } = new Domaine();
        private async Task Save()
        {
            Domaine Env;
            if (Domaine.DomaineId > 0)
            {
                Env = await domaineService.PutAsync(Domaine.DomaineId, Domaine);
                message = "Domaine mis à jour avec succès.";
                Severity = Severity.Success;
                error = true;
            }
            else
            {
                Env = await domaineService.PostAsync(Domaine);
                message = "Domaine enregistré avec succès.";
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
