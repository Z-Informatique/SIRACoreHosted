using Microsoft.AspNetCore.Components;
using MudBlazor;
using Radzen.Blazor;
using TestCoreHosted.Client.Pages.Parametres.Ba;
using TestCoreHosted.Client.Shared;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.Pages.Parametres.Ba
{
    public partial class ListeBA
    {
        [CascadingParameter] MudDialogInstance MudDialogInstance { get; set; }

        public List<Banalytic> BAnalytics { get; set; } = new List<Banalytic>();
        RadzenDataGrid<Banalytic> grid;
        private bool _loading { get; set; }
        async Task LoadData()
        {
            _loading = true;
            BAnalytics = await BAnalyticService.GetAsync();
            _loading = false;
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            StateHasChanged();
        }

        async Task AjoutModifier(Banalytic BAnalytic = null)
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
            if (BAnalytic != null)
            {
                Titre = "Modifier un Business analytic";
                parameters = new DialogParameters { ["BAnalytic"] = BAnalytic };
            }
            else
            {
                Titre = "Nouveau Business analytic";
            }
            var dialog = dialogService.Show<AjouterBAnalytic>(Titre, parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await LoadData();
            }
        }

        async Task Delete(Banalytic BAnalytic)
        {
            if (BAnalytic == null)
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
                var delete = await BAnalyticService.Delete(BAnalytic.Id);
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
