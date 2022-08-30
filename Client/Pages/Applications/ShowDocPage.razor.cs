using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Radzen.Blazor;
using System.Net.Http.Json;
using TestCoreHosted.Client.Shared;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.Pages.Applications
{
    public partial class ShowDocPage
    {
        [CascadingParameter] MudDialogInstance MudDialogInstance { get; set; }
        [Parameter] public Application Application { get; set; } = new Application();
        public Documentation Documentation { get; set; } = new Documentation();
        RadzenDataGrid<Documentation> grid;
        private bool _loading { get; set; } = false;
        public List<Documentation> Documentations { get; set; } = new List<Documentation>();
        async Task getData()
        {
            _loading = true;
            Documentations = await documentationService.GetAsyncByAppId(Application.AppId);
            _loading = false;
            StateHasChanged();
        }
        protected override async Task OnInitializedAsync()
        {
            await getData();
            StateHasChanged();
        }
        private string GetLink(Documentation documentation)
        {
            //return env.ContentRootPath + $"Documentations/Applications/{documentation.AppId}/{documentation.NameFile}";
            return documentation.NameFile;
        }
        async Task Delete(Documentation documentation)
        {
            if (documentation == null)
            {
                snackBar.Add("Votre sélection est vide", Severity.Info);
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
                var delete = await documentationService.Delete(documentation.Id);
                if (delete)
                {
                    await getData();
                    snackBar.Add("Suppression effectuée avec succès.", Severity.Success);
                }
                else
                {
                    snackBar.Add("Une erreur est survenue lors de la suppression.", Severity.Warning);
                }
            }
            StateHasChanged();
        }

        /**
         * Traitement de l'upload des fichiers
         */

        private bool Clearing = false;
        private static string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full";
        private string DragClass = DefaultDragClass;
        private List<string> fileNames = new List<string>();

        List<string> imgUrls = new List<string>();
        List<UploadedFile> uploadedFiles = new List<UploadedFile>();
        private async void OnInputFileChanged(InputFileChangeEventArgs files)
        {
            ClearDragClass();
            uploadedFiles.Clear();
            foreach (IBrowserFile imgFile in files.GetMultipleFiles())
            {
                var buffers = new byte[imgFile.Size];
                await imgFile.OpenReadStream().ReadAsync(buffers);
                string imageType = imgFile.ContentType;
                string imgUrl = $"data:{imageType};base64,{Convert.ToBase64String(buffers)}";
                imgUrls.Add(imgUrl);
                fileNames.Add(imgFile.Name);
                uploadedFiles.Add(new UploadedFile
                {
                    Id = Application.AppId,
                    FileContent = buffers,
                    FileName = imgFile.Name,
                    FileType = imageType
                });
            }
            StateHasChanged();
        }
        private async Task Clear()
        {
            Clearing = true;
            fileNames.Clear();
            ClearDragClass();
            await Task.Delay(100);
            Clearing = false;
        }
        private async void Upload()
        {
            if (uploadedFiles.Count == 0)
            {
                snackBar.Add("Vous devez charger au moins un fichier", Severity.Warning);
                return;
            }

            snackBar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
            if (uploadedFiles.Count > 0)
            {
                var payload = new SaveFile { Files = uploadedFiles };
                var result = await client.PostAsJsonAsync("api/FileUpload/save-file", payload);
                string message = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    snackBar.Add(message, Severity.Success);
                }
                else
                {
                    snackBar.Add(result.StatusCode + ", " + message, Severity.Error);
                }
                await getData();
                ClearDragClass();
                await Clear();
            }
            StateHasChanged();
        }

        private void SetDragClass()
        {
            DragClass = $"{DefaultDragClass} mud-border-primary";
        }

        private void ClearDragClass()
        {
            DragClass = DefaultDragClass;
        }
    }
}
