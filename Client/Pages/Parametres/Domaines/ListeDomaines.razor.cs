using MudBlazor;
using Microsoft.AspNetCore.Components;
using TestCoreHosted.Shared.Models;
using Radzen.Blazor;
using TestCoreHosted.Client.Shared;

namespace TestCoreHosted.Client.Pages.Parametres.Domaines
{
    public partial class ListeDomaines
    {
        [CascadingParameter] MudDialogInstance MudDialogInstance { get; set; }
        private bool _loading { get; set; }
        public List<Domaine> Domaines { get; set; } = new List<Domaine>();
        RadzenDataGrid<Domaine> grid;

        async Task LoadData()
        {
            _loading = true;
            Domaines = await domaineService.GetAsync();
            _loading = false;
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            StateHasChanged();
        }

        async Task AjoutModifier(Domaine Domaine = null)
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
            if (Domaine != null)
            {
                Titre = "Modifier domaine";
                parameters = new DialogParameters { ["Domaine"] = Domaine };
            }
            else
            {
                Titre = "Nouveau domaine";
            }
            var dialog = dialogService.Show<AjouterDomaine>(Titre, parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await LoadData();
            }
        }

        async Task Delete(Domaine Domaine)
        {
            if (Domaine == null)
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
                var delete = await domaineService.Delete(Domaine.DomaineId);
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
