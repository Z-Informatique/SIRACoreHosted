using TestCoreHosted.Client.Data;

namespace TestCoreHosted.Client.Shared
{
    public partial class MainLayout
    {
        private MudblazorTheme _theme = new();

        public bool _drawerOpen = false;
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
