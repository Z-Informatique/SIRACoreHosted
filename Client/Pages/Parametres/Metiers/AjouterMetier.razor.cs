using Microsoft.AspNetCore.Components;
using MudBlazor;
using TestCoreHosted.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestCoreHosted.Client.Pages.Parametres.Metiers
{
    public partial class AjouterMetier
    {
        [CascadingParameter] MudDialogInstance MudDialogInstance { get; set; }
        private bool error { get; set; }
        public bool IsBusy { get; set; } = false;
        private string message { get; set; }
        private Severity Severity { get; set; }
        [Parameter] public Metier Metier { get; set; } = new Metier();
        private async Task Save()
        {
            Metier Env;
            if (Metier.MetierId > 0)
            {
                Env = await metierService.PutAsync(Metier.MetierId, Metier);
                message = "Metier mis à jour avec succès.";
                Severity = Severity.Success;
                error = true;
            }
            else
            {
                Env = await metierService.PostAsync(Metier);
                message = "Metier enregistré avec succès.";
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
