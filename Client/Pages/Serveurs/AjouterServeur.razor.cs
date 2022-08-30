using Microsoft.AspNetCore.Components;
using MudBlazor;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.Pages.Serveurs
{
    public partial class AjouterServeur
    {
        [CascadingParameter] MudDialogInstance MudDialogInstance { get; set; }
        private bool error { get; set; }
        public bool IsBusy { get; set; } = false;
        private string message { get; set; }
        private Severity Severity { get; set; }

        private int _idTypeOs;
        public int IdTypeOs
        {
            get { return _idTypeOs; }
            set
            {
                _idTypeOs = value;
                if (_idTypeOs > 0)
                {
                    loadOsystem(_idTypeOs);
                }
            }
        }

        [Parameter] public Serveur Serveur { get; set; } = new Serveur();
        public List<Environnement> Environnements { get; set; } = new List<Environnement>();
        public List<TypeO> TypeOs { get; set; } = new List<TypeO>();
        public List<OSystem> OSystems { get; set; } = new List<OSystem>();
        private async Task Save()
        {
            Serveur serveur;
            Serveur.OsId = IdTypeOs;
            if (Serveur.ServeurId > 0)
            {
                serveur = await serveurService.PutAsync(Serveur.ServeurId, Serveur);
                message = "Serveur mis à jour avec succès.";
                Severity = Severity.Success;
                error = true;
            }
            else
            {
                serveur = await serveurService.PostAsync(Serveur);
                message = "Serveur enregistré avec succès.";
                Severity = Severity.Success;
                error = true;
            }

            if (serveur != null)
            {
                snackBar.Add(message, Severity);
                MudDialogInstance.Close(DialogResult.Ok(true));
            }
            else
            {
                message = "Une erreur s'est produite lors du traitement.";
                Severity = Severity.Error;
                snackBar.Add(message, Severity);
                error = true;
            }
        }
        async void loadOsystem(int IdTypeOs)
        {
            OSystems = await oSystemService.GetAsyncItemByIdTypeOs(IdTypeOs);
            StateHasChanged();
        }
        async Task Load()
        {
            Environnements = await envService.GetAsync();
            TypeOs = await typeOsService.GetAsync();


            if (Serveur == null)
            {
                Serveur.MigDate = DateTime.Today;
            }
            else
            {
                IdTypeOs = Serveur.OsId;
            }

            StateHasChanged();
        }
        protected override async Task OnInitializedAsync()
        {
            await Load();
            StateHasChanged();
        }
    }
}
