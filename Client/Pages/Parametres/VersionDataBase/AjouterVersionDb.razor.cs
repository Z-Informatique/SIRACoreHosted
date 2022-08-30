using Microsoft.AspNetCore.Components;
using MudBlazor;
using TestCoreHosted.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestCoreHosted.Client.Pages.Parametres.VersionDataBase
{
    public partial class AjouterVersionDb
    {
        [CascadingParameter] MudDialogInstance MudDialogInstance { get; set; }
        private bool error { get; set; }
        public bool IsBusy { get; set; } = false;
        private string message { get; set; }
        private Severity Severity { get; set; }
        [Parameter] public VersionDb VersionDb { get; set; } = new VersionDb();
        public List<Db> Dbs { get; set; } = new List<Db>();
        private async Task Save()
        {
            VersionDb versionDb;
            if (VersionDb.VdbId > 0)
            {
                versionDb = await versionDbService.PutAsync(VersionDb.VdbId, VersionDb);
                message = "Version de base de données mise à jour avec succès.";
                Severity = Severity.Success;
                error = true;
            }
            else
            {
                versionDb = await versionDbService.PostAsync(VersionDb);
                message = "Version de base de données enregistré avec succès.";
                Severity = Severity.Success;
                error = true;
            }

            if (versionDb != null)
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
            Dbs = await dbService.GetAsync();
        }
        protected override async Task OnInitializedAsync()
        {
            await Load();
            StateHasChanged();
        }
    }
}
