using MudBlazor;
using Microsoft.AspNetCore.Components;
using TestCoreHosted.Shared.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Radzen.Blazor;
using TestCoreHosted.Client.Shared;

namespace TestCoreHosted.Client.Pages.Parametres.Metiers
{
    public partial class ListeMetiers
    {
        [CascadingParameter] MudDialogInstance MudDialogInstance { get; set; }

        public List<Metier> Metiers { get; set; } = new List<Metier>();
        RadzenDataGrid<Metier> grid;
        private bool _loading { get; set; }
        async Task LoadData()
        {
            _loading = true;
            Metiers = await metierService.GetAsync();
            _loading = false;
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            StateHasChanged();
        }

        async Task AjoutModifier(Metier Metier = null)
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
            if (Metier != null)
            {
                Titre = "Modifier le metier";
                parameters = new DialogParameters { ["Metier"] = Metier };
            }
            else
            {
                Titre = "Nouveau metier";
            }
            var dialog = dialogService.Show<AjouterMetier>(Titre, parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await LoadData();
            }
        }

        async Task Delete(Metier Metier)
        {
            if (Metier == null)
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
                var delete = await metierService.Delete(Metier.MetierId);
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
