using MudBlazor;
using Microsoft.AspNetCore.Components;
using TestCoreHosted.Shared.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Radzen.Blazor;
using TestCoreHosted.Client.Shared;

namespace TestCoreHosted.Client.Pages.Parametres.DataBase
{
    public partial class ListeDb
    {
        [CascadingParameter] MudDialogInstance MudDialogInstance { get; set; }
        private bool _loading { get; set; }
        public List<Db> Dbs { get; set; } = new List<Db>();
        RadzenDataGrid<Db> grid;

        async Task LoadData()
        {
            _loading = true;
            Dbs = await dbService.GetAsync();
            _loading = false;
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            StateHasChanged();
        }

        async Task AjoutModifier(Db db = null)
        {
            MudBlazor.DialogOptions options = new MudBlazor.DialogOptions
            {
                DisableBackdropClick = true,
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };
            DialogParameters parameters = new DialogParameters();
            string Titre;
            if (db != null)
            {
                Titre = "Modifier le moteur de base de données";
                parameters = new DialogParameters { ["DbData"] = db };
            }
            else
            {
                Titre = "Nouveau moteur de base de données";
            }
            var dialog = dialogService.Show<AjouterDb>(Titre, parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await LoadData();
            }
        }

        async Task Delete(Db db)
        {
            if (db == null)
            {
                snackBar.Add("Votre sélection est vide", Severity.Info);
                return;
            }
            var parameters = new DialogParameters();
            parameters.Add("Texte", "Confirmer la suppression.");
            parameters.Add("ButtonText", "Supprimer");
            parameters.Add("Color", Color.Error);
            parameters.Add("Variant", Variant.Text);

            var options = new MudBlazor.DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
            var dialog = dialogService.Show<MessageDialog>("Alerte !", parameters, options);

            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var delete = await dbService.Delete(db.Id);
                if (delete)
                {
                    await LoadData();
                    snackBar.Add("Suppression effectuée avec succès.", Severity.Success);
                }
                else
                {
                    snackBar.Add("Une erreur est survenue lors de la suppression.", Severity.Warning);
                }
            }
            StateHasChanged();
        }
    }
}
