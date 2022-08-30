using Microsoft.AspNetCore.Components;
using MudBlazor;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.Pages.BaseDeDonnees
{
    public partial class AjouterDataBase
    {
        [CascadingParameter] MudDialogInstance MudDialogInstance { get; set; }
        private bool error { get; set; }
        public bool IsBusy { get; set; } = false;
        private string message { get; set; }
        private Severity Severity { get; set; }

        private int _idDb;
        public int IdDb
        {
            get { return _idDb; }
            set
            {
                _idDb = value;
                if (_idDb > 0)
                {
                    loadVersionDb(_idDb);
                    Db = Dbs.Where(x => x.Id == _idDb).FirstOrDefault();
                }
            }
        }

        [Parameter] public DataBase DataBase { get; set; } = new DataBase();
        public List<Environnement> Environnements { get; set; } = new List<Environnement>();
        public List<Application> Applications { get; set; } = new List<Application>();
        public List<Serveur> Serveurs { get; set; } = new List<Serveur>();
        public List<Db> Dbs { get; set; } = new List<Db>();
        public List<VersionDb> VersionDbs { get; set; } = new List<VersionDb>();

        private Environnement Environnement { get; set; } = new Environnement();
        private Application Application { get; set; } = new Application();
        private Serveur Serveur { get; set; } = new Serveur();
        private Db Db { get; set; } = new Db();
        private VersionDb VersionDb { get; set; } = new VersionDb();


        private int _VersionDb;
        public int versionDb
        {
            get { return _VersionDb; }
            set
            {
                _VersionDb = value;
                if (_VersionDb > 0)
                {
                    VersionDb = VersionDbs.Where(x => x.VdbId == _VersionDb).FirstOrDefault();
                }
            }
        }

        private int _AppId;
        public int AppId
        {
            get { return _AppId; }
            set
            {
                _AppId = value;
                if (_AppId > 0)
                {
                    Application = Applications.Where(x => x.AppId == _AppId).FirstOrDefault();
                }
            }
        }

        private int _ServeurId;
        public int ServeurId
        {
            get { return _ServeurId; }
            set
            {
                _ServeurId = value;
                if (_ServeurId > 0)
                {
                    Serveur = Serveurs.Where(x => x.ServeurId == _ServeurId).FirstOrDefault();
                }
            }
        }

        private int _EnvId;
        public int EnvId
        {
            get { return _EnvId; }
            set
            {
                _EnvId = value;
                if (_EnvId > 0)
                {
                    Environnement = Environnements.Where(x => x.EnvId == _EnvId).FirstOrDefault();
                }
            }
        }

        private async Task Save()
        {
            IsBusy = true;
            DataBase dataBase;
            DataBase.EnvId = EnvId;
            DataBase.ServeurId = ServeurId;
            DataBase.AppId = AppId;
            DataBase.VersionDbId = versionDb;

            if (DataBase.DataId > 0)
            {
                dataBase = await dataBaseService.PutAsync(DataBase.DataId, DataBase);
                message = "Base de données mise à jour avec succès.";
                Severity = Severity.Success;
                error = true;
            }
            else
            {
                dataBase = await dataBaseService.PostAsync(DataBase);
                message = "DataBase enregistré avec succès.";
                Severity = Severity.Success;
                error = true;
            }

            if (dataBase != null)
            {
                snackBar.Add(message, Severity);
                IsBusy = false;
                MudDialogInstance.Close(DialogResult.Ok(true));
            }
            else
            {
                message = "Une erreur s'est produite lors du traitement.";
                Severity = Severity.Error;
                snackBar.Add(message, Severity);
                error = true;
                IsBusy = false;
            }
            IsBusy = false;
        }
        async void loadVersionDb(int idDb)
        {
            VersionDbs = await versionDbService.getItemsByMoteurDb(idDb);
            StateHasChanged();
        }
        async Task Load()
        {
            Environnements = await envService.GetAsync();
            Applications = await applicationService.GetAsync();
            Serveurs = await serveurService.GetAsync();
            Dbs = await dbService.GetAsync();


            if (DataBase == null)
            {
                DataBase.MigDate = DateTime.Today;
            }
            else
            {
                EnvId = DataBase.EnvId;
                AppId = DataBase.AppId;
                ServeurId = DataBase.ServeurId;
                versionDb = DataBase.VersionDbId;
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
