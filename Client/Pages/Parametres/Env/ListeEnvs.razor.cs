using MudBlazor;
using Microsoft.AspNetCore.Components;
using TestCoreHosted.Shared.Models;
using Radzen.Blazor;
using TestCoreHosted.Client.Shared;

namespace TestCoreHosted.Client.Pages.Parametres.Env
{
    public partial class ListeEnvs
    {
        [CascadingParameter] MudDialogInstance MudDialogInstance { get; set; }
        private bool _loading { get; set; }
        public List<Environnement> Environnements { get; set; } = new List<Environnement>();
        RadzenDataGrid<Environnement> grid;

        async Task LoadData()
        {
            _loading = true;
            Environnements = await envService.GetAsync();
            _loading = false;
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            StateHasChanged();
        }

        async Task AjoutModifier(Environnement Environnement = null)
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
            if (Environnement != null)
            {
                Titre = "Modifier l'environnement";
                parameters = new DialogParameters { ["Environnement"] = Environnement };
            }
            else
            {
                Titre = "Nouvel environnement";
            }
            var dialog = dialogService.Show<AjouterEnvironnement>(Titre, parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await LoadData();
            }
        }

        async Task Delete(Environnement Environnement)
        {
            if (Environnement == null)
            {
                snackBar.Add("Votre sélection est vide", Severity.Info);
                return;
            }
            var parameters = new DialogParameters();
            parameters.Add("Texte", "Confirmer la suppression.");
            parameters.Add("ButtonText", "Supprimer");
            parameters.Add("Color", Color.Error);
            parameters.Add("Variant", Variant.Text);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
            var dialog = dialogService.Show<MessageDialog>("Alerte !", parameters, options);

            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var delete = await envService.Delete(Environnement.EnvId);
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
