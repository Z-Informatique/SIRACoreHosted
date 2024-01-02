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
        SearchModel SearchModel { get; set; } = new SearchModel();
        public string Search { get; set; }
        public List<Application> _getApplication { get; set; } = new List<Application>();
        public List<Application> _getApplicationFilter { get; set; } = new List<Application>();
        public List<Application> _getApplication1 { get; set; } = new List<Application>();
        public List<Application> _getApplication2 { get; set; } = new List<Application>();
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
            _getApplication2 = await applications.GetListeAsync();
            _getApplication1 = await applications.GetAsync();

            if (_getApplication1.Count >= _getApplication2.Count)
            {
                _getApplication = _getApplication1;
            }
            else
            {
                foreach (var application in _getApplication2)
                {
                    if (application.IdBa == 0 || application.IdBa == null)
                    {
                        await UpdateBa(application);
                        break;
                    }
                }
            }

            _loading = false;
            StateHasChanged();
        }

        private async Task UpdateBa(Application application)
        {
            MudBlazor.DialogOptions options = new MudBlazor.DialogOptions
            {
                DisableBackdropClick = true,
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true,
            };
            DialogParameters parameters = new DialogParameters();
            parameters = new DialogParameters { ["Application"] = application };

            var dialog = dialogService.Show<UpdateBaForm>("Business Analytic : " + application.Titre, parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await Load();
            }
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
        void TextBoxChanged()
        {
            if (string.IsNullOrEmpty(SearchModel.Search))
            {
                snackbar.Add("Le champ recherche est vide", Severity.Error);
                return;
            }
            if(_getApplicationFilter.Count == 0)
                _getApplicationFilter = _getApplication;



            if (SearchModel.Search.ToLower() == "en ligne")
            {
                _getApplication = _getApplicationFilter.FindAll(x => x.Etat == "2").ToList();
            }
            else if (SearchModel.Search.ToLower() == "démobilisé")
            {
                _getApplication = _getApplicationFilter.FindAll(x => x.Etat == "1").ToList();
            }
            else if (SearchModel.Search.ToLower() == "migrée" || SearchModel.Search.ToLower().Contains("migré"))
            {
                _getApplication = _getApplicationFilter.FindAll(x => x.Etat == "0").ToList();
            }
            else
            {

                _getApplication = _getApplicationFilter.FindAll(x => x.Titre.ToLower().Contains(SearchModel.Search.ToLower())
                                                                || x.Domaine.DTitle.ToLower().Contains(SearchModel.Search.ToLower())
                                                                || x.Metier.Title.ToLower().Contains(SearchModel.Search.ToLower())
                                                                || x.Architecture.ToLower().Contains(SearchModel.Search.ToLower())
                                                                || x.SiteApp.ToLower().Contains(SearchModel.Search.ToLower())
                                                                || x.VersionApp.ToLower().Contains(SearchModel.Search.ToLower())
                                                                ).ToList();
            }

            StateHasChanged();
        }
        void RefreshList()
        {
            if (_getApplicationFilter.Count > 0)
            {
                _getApplication = _getApplicationFilter;
            }

            StateHasChanged();
        }
        protected override async Task OnInitializedAsync()
        {
            await Load();
            StateHasChanged();
        }
        async Task AjoutModifier(Application? application = null)
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
            var parameters = new DialogParameters
            {
                { "Texte", "Confirmer la suppression." },
                { "ButtonText", "Supprimer" },
                { "Color", Color.Error },
                { "Variant", MudBlazor.Variant.Text }
            };

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
    public class SearchModel
    {
        public string Search { get; set; }
    }
}
