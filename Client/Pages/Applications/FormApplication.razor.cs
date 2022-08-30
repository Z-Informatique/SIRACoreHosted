﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using Radzen.Blazor;
using TestCoreHosted.Client.Shared;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.Pages.Applications
{
    public partial class FormApplication
    {
        [CascadingParameter] MudDialogInstance MudDialogInstance { get; set; }
        private bool error { get; set; }
        public bool IsBusy { get; set; } = false;
        private string message { get; set; }
        private Severity Severity { get; set; }
        private IList<Serveur> selectedItems1 { get; set; } = new List<Serveur>();
        [Parameter] public Application Application { get; set; } = new Application();
        public List<Serveur> Serveurs { get; set; } = new List<Serveur>();
        public List<Domaine> Domaines { get; set; } = new List<Domaine>();
        public List<Metier> Metiers { get; set; } = new List<Metier>();
        RadzenDataGrid<Serveur> grid;
        bool allowRowSelectOnRowClick = true;
        private async Task Save()
        {
            var parameters = new DialogParameters();
            parameters.Add("Texte", "Vous n'avez sélectionner aucun serveur pour cette application. Souhaitez-vous continuer ?");
            parameters.Add("ButtonText", "Continuer");
            parameters.Add("Color", Color.Info);
            parameters.Add("Variant", Variant.Text);

            Application application = new Application();

            if (selectedItems1.Count == 0)
            {
                var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
                var dialog = dialogService.Show<MessageDialog>("Alerte !", parameters, options);

                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    if (Application.AppId > 0)
                    {
                        application = await applicationsService.PutAsync(Application.AppId, Application);
                        message = "Application mis à jour avec succès.";
                        Severity = Severity.Success;
                        error = true;
                    }
                    else
                    {
                        application = await applicationsService.PostAsync(Application);
                        message = "Application enregistré avec succès.";
                        Severity = Severity.Success;
                        error = true;
                    }
                }
                else
                {
                    MudDialogInstance.Close(DialogResult.Ok(true));
                }
            }
            else
            {

                if (Application.AppId > 0)
                {
                    Application.ServeurId = String.Join(",", getServeurListe());
                    application = await applicationsService.PutAsync(Application.AppId, Application);
                    message = "Application mis à jour avec succès.";
                    Severity = Severity.Success;
                    error = true;
                }
                else
                {
                    Application.ServeurId = String.Join(",", getServeurListe());
                    application = await applicationsService.PostAsync(Application);
                    message = "Application enregistré avec succès.";
                    Severity = Severity.Success;
                    error = true;
                }
            }

            if (application != null)
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
        private List<string> getServeurListe()
        {
            List<string> ServeurId = new List<string>();
            foreach (var item in selectedItems1)
            {
                ServeurId.Add(item.ServeurId.ToString());
            }
            return ServeurId;
        }
        async Task Load()
        {
            Serveurs = await serveurService.GetAsync();
            Domaines = await domaineService.GetAsync();
            Metiers = await metierService.GetAsync();

            if (Application == null)
            {
                Application.MigDate = DateTime.Today;
            }
            else
            {
                if (!string.IsNullOrEmpty(Application.ServeurId))
                {
                    List<string> results = Application.ServeurId.Split(',').ToList();

                    if (results.Count > 0)
                    {
                        selectedItems1.Clear();
                        foreach (var item in results)
                        {
                            var result = await serveurService.GetAsyncItem(Convert.ToInt32(item));
                            if (result != null)
                            {
                                selectedItems1.Add(result);
                            }
                        }
                    }
                }
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