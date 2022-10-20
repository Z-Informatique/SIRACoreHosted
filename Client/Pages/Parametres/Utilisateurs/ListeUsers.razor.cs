using Microsoft.AspNetCore.Components;
using MudBlazor;
using Radzen.Blazor;
using TestCoreHosted.Client.Services;
using TestCoreHosted.Client.Shared;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.Pages.Parametres.Utilisateurs
{
    public partial class ListeUsers
    {
        [CascadingParameter] MudDialogInstance MudDialogInstance { get; set; }

        public List<User> Users { get; set; } = new List<User>();
        RadzenDataGrid<User> grid;
        private bool _loading { get; set; }
        async Task LoadData()
        {
            _loading = true;
            Users = await UserService.GetAsync();
            _loading = false;
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            StateHasChanged();
        }

        async Task AjoutModifier(User User = null)
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
            if (User != null)
            {
                Titre = "Modifier l'utilisateur";
                parameters = new DialogParameters { ["User"] = User };
            }
            else
            {
                Titre = "Nouvel utilisateur";
            }
            var dialog = dialogService.Show<AjouterUser>(Titre, parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await LoadData();
            }
        }

        async Task Delete(User User)
        {
            if (User == null)
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
                var delete = await UserService.Delete(User.Id);
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
