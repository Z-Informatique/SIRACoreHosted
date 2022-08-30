using MudBlazor;
using TestCoreHosted.Client.Pages.Parametres.DataBase;
using TestCoreHosted.Client.Pages.Parametres.Domaines;
using TestCoreHosted.Client.Pages.Parametres.Env;
using TestCoreHosted.Client.Pages.Parametres.Metiers;
using TestCoreHosted.Client.Pages.Parametres.SystemesExploitation;
using TestCoreHosted.Client.Pages.Parametres.VersionDataBase;

namespace TestCoreHosted.Client.Pages.Parametres
{
    public partial class Index
    {
        async Task getDataDb()
        {
            DialogOptions options = new DialogOptions
            {
                DisableBackdropClick = true,
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };
            var dialog = dialogService.Show<ListeDb>("Moteur de bases de données", options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {

            }
            StateHasChanged();
        }

        async Task getVersionDb()
        {
            DialogOptions options = new DialogOptions
            {
                DisableBackdropClick = true,
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };
            var dialog = dialogService.Show<ListeVersionDb>("Version de bases de données", options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {

            }
            StateHasChanged();
        }

        async Task getOs()
        {
            DialogOptions options = new DialogOptions
            {
                DisableBackdropClick = true,
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };
            var dialog = dialogService.Show<ListeOs>("Systèmes d'exploitation", options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {

            }
            StateHasChanged();
        }

        async Task getVersionOs()
        {
            DialogOptions options = new DialogOptions
            {
                DisableBackdropClick = true,
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };
            var dialog = dialogService.Show<ListeVersionOs>("Versions systèmes d'exploitation", options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {

            }
            StateHasChanged();
        }

        async Task getEnv()
        {
            DialogOptions options = new DialogOptions
            {
                DisableBackdropClick = true,
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };
            var dialog = dialogService.Show<ListeEnvs>("Environnements", options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {

            }
            StateHasChanged();
        }

        async Task getDomaine()
        {
            DialogOptions options = new DialogOptions
            {
                DisableBackdropClick = true,
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };
            var dialog = dialogService.Show<ListeDomaines>("Domaines", options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {

            }
            StateHasChanged();
        }

        async Task getMetier()
        {
            DialogOptions options = new DialogOptions
            {
                DisableBackdropClick = true,
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };
            var dialog = dialogService.Show<ListeMetiers>("Metiers", options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {

            }
            StateHasChanged();
        }

    }
}
