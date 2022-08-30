using MudBlazor;
using TestCoreHosted.Shared.Models;
using Radzen;
using Radzen.Blazor;
using TestCoreHosted.Client.Shared;

namespace TestCoreHosted.Client.Pages.BaseDeDonnees
{
    public partial class Index
    {
        private bool _loading = false;
        RadzenDataGrid<DataBase> grid;
        private List<DataBase> _getDataBases { get; set; } = new List<DataBase>();

        public string setEtat(int Etat)
        {
            if (Etat.Equals(0))
            {
                return "Migré";
            }
            else if (Etat.Equals(1))
            {
                return "En ligne";
            }
            else if (Etat.Equals(2))
            {
                return "Démobilisée";
            }
            return "Aucun";
        }
        public Color setColor(int Etat)
        {
            if (Etat.Equals(0))
            {
                return Color.Info;
            }
            else if (Etat.Equals(1))
            {
                return Color.Primary;
            }
            else if (Etat.Equals(2))
            {
                return Color.Error;
            }
            return Color.Default;
        }
        protected async Task Load()
        {
            _loading = true;
            _getDataBases = await dataBaseService.GetAsync();
            _loading = false;
        }
        public void Export(string type)
        {
            service.Export("databases", type, new Query()
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
        async Task AjoutModifier(DataBase dataBase = null)
        {
            MudBlazor.DialogOptions options = new MudBlazor.DialogOptions
            {
                DisableBackdropClick = true,
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true
            };
            DialogParameters parameters = new DialogParameters();
            string Titre;
            if (dataBase != null)
            {
                Titre = "Modifier la base de données";
                parameters = new DialogParameters { ["DataBase"] = dataBase };
            }
            else
            {
                Titre = "Nouvelle base de données";
            }
            var dialog = dialogService.Show<AjouterDataBase>(Titre, parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await Load();
            }
        }
        async Task Delete(DataBase dataBase)
        {
            if (dataBase == null)
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
                var delete = await dataBaseService.Delete(dataBase.DataId);
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
            if (_getDataBases.Count == 0)
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
            parameters = new DialogParameters { ["DataBases"] = _getDataBases };
            var dialog = dialogService.Show<DataAnalytiquePage>("Données d'analyse", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {

            }
        }
    }
}
