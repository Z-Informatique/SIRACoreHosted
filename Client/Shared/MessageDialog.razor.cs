using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace TestCoreHosted.Client.Shared
{
    public partial class MessageDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        [Parameter] public string Texte { get; set; }
        [Parameter] public string ButtonText { get; set; }
        [Parameter] public Color Color { get; set; }
        [Parameter] public Variant Variant { get; set; }
        void Submit() => MudDialog.Close(DialogResult.Ok(true));
        void Cancel() => MudDialog.Cancel();
    }
}
