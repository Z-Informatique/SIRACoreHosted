using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using TestCoreHosted.Client.Data;
using MudBlazor;

namespace TestCoreHosted.Client.Shared
{
    public partial class MainLayout
    {
        private MudblazorTheme _theme = new();

        public bool _drawerOpen = false;
        public bool _show = false;
        [CascadingParameter]
        Task<AuthenticationState> authenticationState { get; set; }
        public string Name { get; set; }
        protected override async Task OnParametersSetAsync()
        {
            var user = (await authenticationState).User.Identity;

            if (!user.IsAuthenticated)
            {
                _show = false;
            }
            else
            {
                Name = user.Name;
                _show = true;
            }
        }
        async Task Logout()
        {
            var parameters = new DialogParameters
            {
                { "Texte", "Voulez-vous vraiment vous déconnecter ?" },
                { "ButtonText", "OUI" },
                { "Color", Color.Error },
                { "Variant", Variant.Text }
            };

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
            var dialog = dialogService.Show<MessageDialog>("Déconnexion !", parameters, options);

            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await authStateProvider.MarkUserAsLoggOut();
                string url = navigationManager.Uri;
                navigationManager.NavigateTo(url);
                snackBar.Add("A très bientôt !", Severity.Info);
            }
        }
        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        protected override void OnInitialized()
        {
            StateHasChanged();
        }
    }
}
