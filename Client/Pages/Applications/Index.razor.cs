using MudBlazor;
using Radzen;
using Radzen.Blazor;
using TestCoreHosted.Client.Shared;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.Pages.Applications
{
    public partial class Index
    {
        RadzenDataGrid<Application> grid;
        public List<Application> _getApplication { get; set; } = new List<Application>();
        private bool _loading;
        public string setEtat(int Etat)
        {
            if (Etat.Equals(1))
            {
                return "Démobilisée";
            }
            else if (Etat.Equals(2))
            {
                return "En ligne";
            }
            else if (Etat.Equals(0))
            {
                return "Migrée";
            }
            return "Aucun";
        }
        public Color setColor(int Etat)
        {
            if (Etat.Equals(1))
            {
                return Color.Error;
            }
            else if (Etat.Equals(2))
            {
                return Color.Primary;
            }
            else if (Etat.Equals(0))
            {
                return Color.Success;
            }
            return Color.Default;
        }
        protected async Task Load()
        {
            _loading = true;
            _getApplication = await applications.GetAsync();
            _loading = false;
            StateHasChanged();
        }
        public void Export(string type)
        {
            service.Export("Applications", type, new Query()
            {
                OrderBy = grid.Query.OrderBy,
                Filter = grid.Query.Filter,
                Select = string.Join(",", grid.ColumnsCollection.Where(c => c.GetVisible()).Select(c => c.Property))
            });
        }
        protected override async Task OnInitializedAsync()
        {
            await Load();
            StateHasChanged();
        }
        async Task AjoutModifier(Application application = null)
        {
            MudBlazor.DialogOptions options = new MudBlazor.DialogOptions
            {
                DisableBackdropClick = true,
                CloseButton = true,
                MaxWidth = MaxWidth.Large,
                FullWidth = true,
            };
            DialogParameters parameters = new DialogParameters();
            string Titre;
            if (application != null)
            {
                Titre = "Modifier l'application";
                parameters = new DialogParameters { ["Application"] = application };
            }
            else
            {
                Titre = "Nouvelle application";
            }
            var dialog = dialogService.Show<FormApplication>(Titre, parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await Load();
                await grid.Reload();
            }
        }
        async Task Delete(Application application)
        {
            if (application == null)
            {
                snackbar.Add("Votre sélection est vide", Severity.Info);
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
                var delete = await applications.Delete(application.AppId);
                if (delete)
                {
                    await Load();
                    snackbar.Add("Suppression effectuée avec succès.", Severity.Success);
                }
                else
                {
                    snackbar.Add("Une erreur est survenue lors de la suppression.", Severity.Warning);
                }
            }
            StateHasChanged();
        }
        async Task DataAnalytique()
        {
            if (_getApplication.Count == 0)
            {
                snackbar.Add("Aucune donnée à afficher car la liste est vide.", Severity.Info);
                return;
            }
            MudBlazor.DialogOptions options = new MudBlazor.DialogOptions
            {
                DisableBackdropClick = true,
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true
            };
            DialogParameters parameters = new DialogParameters();
            parameters = new DialogParameters { ["Applications"] = _getApplication };
            var dialog = dialogService.Show<DataAnalytiquePage>("Données d'analyse", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {

            }
        }

        async Task DocPage(Application application)
        {
            if (application == null)
            {
                snackbar.Add("Votre sélection est nulle, veuillez réessayer.", Severity.Warning);
                return;
            }
            MudBlazor.DialogOptions options = new MudBlazor.DialogOptions
            {
                DisableBackdropClick = true,
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true
            };
            DialogParameters parameters = new DialogParameters();
            parameters = new DialogParameters { ["Application"] = application };
            var dialog = dialogService.Show<ShowDocPage>($"Documentation : {application.Titre}", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {

            }
        }
        async Task ShowServerPage(Application application)
        {
            if (string.IsNullOrEmpty(application.ServeurId))
            {
                snackbar.Add("Cette application n'est liée à aucun serveur.", Severity.Info);
                return;
            }
            if (application == null)
            {
                snackbar.Add("Votre sélection est nulle.", Severity.Warning);
                return;
            }
            MudBlazor.DialogOptions options = new MudBlazor.DialogOptions
            {
                DisableBackdropClick = true,
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true
            };
            DialogParameters parameters = new DialogParameters();
            parameters = new DialogParameters { ["Application"] = application };
            var dialog = dialogService.Show<ShowServerPage>($"Liste des serveurs : {application.Titre}", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {

            }

        }
    }
}
