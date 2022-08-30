using Microsoft.AspNetCore.Components;
using Radzen;

namespace TestCoreHosted.Client.Data
{
    public partial class ExportFileService
    {
        private readonly NavigationManager navigationManager;
        private readonly HttpClient client;
        public ExportFileService(NavigationManager navigationManager, HttpClient client)
        {
            this.navigationManager = navigationManager;
            this.client = client;
        }

        public void Export(string table, string type, Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"/export/TestCoreHosted/{table}/{type}") : $"/export/TestCoreHosted/{table}/{type}", true);
        }
    }
}
