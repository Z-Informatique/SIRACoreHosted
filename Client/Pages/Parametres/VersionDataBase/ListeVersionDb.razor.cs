using MudBlazor;
using Microsoft.AspNetCore.Components;
using TestCoreHosted.Shared.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Radzen.Blazor;
using TestCoreHosted.Client.Shared;

namespace TestCoreHosted.Client.Pages.Parametres.VersionDataBase
{
    public partial class ListeVersionDb
    {
        [CascadingParameter] MudDialogInstance MudDialogInstance { get; set; }

        public List<VersionDb> VersionDbs { get; set; } = new List<VersionDb>();
        RadzenDataGrid<VersionDb> grid;
        private bool _loading { get; set; }
        async Task LoadData()
        {
            _loading = true;
            VersionDbs = await versionDbService.GetAsync();
            _loading = false;
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            StateHasChanged();
        }

        async Task AjoutModifier(VersionDb versionDb = null)
        {
            DialogOptions options = new DialogOptions
            {
                DisableBackdropClick = true,
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };
            DialogParameters parameters = new DialogParameters();
            string Titre;
            if (versionDb != null)
            {
                Titre = "Modifier le moteur de base de données";
                parameters = new DialogParameters { ["VersionDb"] = versionDb };
            }
            else
            {
                Titre = "Nouveau moteur de base de données";
            }
            var dialog = dialogService.Show<AjouterVersionDb>(Titre, parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await LoadData();
            }
        }

        async Task Delete(VersionDb version)
        {
            if (version == null)
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
                var delete = await versionDbService.Delete(version.VdbId);
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
