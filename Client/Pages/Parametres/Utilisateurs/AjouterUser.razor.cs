using Microsoft.AspNetCore.Components;
using MudBlazor;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.Pages.Parametres.Utilisateurs
{
    public partial class AjouterUser
    {
        [CascadingParameter] MudDialogInstance MudDialogInstance { get; set; }
        private bool error { get; set; }
        public bool IsBusy { get; set; } = false;
        private string message { get; set; }
        private Severity Severity { get; set; }
        [Parameter] public User User { get; set; } = new User();
        private async Task Save()
        {
            User Env;
            if (User.Id > 0)
            {
                Env = await UserService.PutAsync(User.Id, User);
                message = "Utilisateur mis à jour avec succès.";
                Severity = Severity.Success;
                error = true;
            }
            else
            {
                Env = await UserService.PostAsync(User);
                message = "Utilisateur enregistré avec succès.";
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
