using MudBlazor;
using Radzen;
using Radzen.Blazor;
using TestCoreHosted.Client.Shared;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.Pages.Serveurs
{
    public partial class Index
    {
        private bool _loading = false;
        RadzenDataGrid<Serveur> grid;
        public List<Serveur> _getServeurs { get; set; } = new List<Serveur>();
        public string setEtat(int Etat)
        {
            if (Etat.Equals(1))
            {
                return "Démobilisé";
            }
            else if (Etat.Equals(2))
            {
                return "Migré";
            }
            else if (Etat.Equals(0))
            {
                return "En maintenance";
            }
            return "En ligne";
        }
        public Color setColor(int Etat)
        {
            if (Etat.Equals(1))
            {
                return Color.Error;
            }
            else if (Etat.Equals(2))
            {
                return Color.Info;
            }
            else if (Etat.Equals(0))
            {
                return Color.Warning;
            }
            return Color.Primary;
        }
        protected async Task Load()
        {
            _loading = true;
            _getServeurs = await serveurService.GetAsync();
            _loading = false;
        }
        public void Export(string type)
        {
            service.Export("Serveurs", type, new Query()
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

        async Task AjoutModifier(Serveur serveur = null)
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
            if (serveur != null)
            {
                Titre = "Modifier le serveur";
                parameters = new DialogParameters { ["Serveur"] = serveur };
            }
            else
            {
                Titre = "Nouveau serveur";
            }
            var dialog = dialogService.Show<AjouterServeur>(Titre, parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                await Load();
            }
        }

        async Task Delete(Serveur Serveur)
        {
            if (Serveur == null)
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
                var delete = await serveurService.Delete(Serveur.ServeurId);
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
            if (_getServeurs.Count == 0)
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
            parameters = new DialogParameters { ["Serveurs"] = _getServeurs };
            var dialog = dialogService.Show<DataAnalytiquePage>("Données d'analyse", parameters, options);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {

            }
        }
    }
}
