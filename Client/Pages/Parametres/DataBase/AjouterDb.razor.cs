using Microsoft.AspNetCore.Components;
using MudBlazor;
using TestCoreHosted.Shared.Models;
using System.Threading.Tasks;

namespace TestCoreHosted.Client.Pages.Parametres.DataBase
{
    public partial class AjouterDb
    {
        [CascadingParameter] MudDialogInstance MudDialogInstance { get; set; }
        private bool error { get; set; }
        public bool IsBusy { get; set; } = false;
        private string message { get; set; }
        private Severity Severity { get; set; }
        [Parameter] public Db DbData { get; set; } = new Db();
        private async Task Save()
        {
            Db db;
            if (DbData.Id > 0)
            {
                db = await dbService.PutAsync(DbData.Id, DbData);
                message = "Moteur de base de données mise à jour avec succès.";
                Severity = Severity.Success;
                error = true;
            }
            else
            {
                db = await dbService.PostAsync(DbData);
                message = "Moteur de base de données enregistré avec succès.";
                Severity = Severity.Success;
                error = true;
            }

            if (db != null)
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
