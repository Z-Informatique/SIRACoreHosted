using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using TestCoreHosted.Client.Data;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.Pages.Auth
{
    public partial class LoginPage
    {
        private string Email { get; set; }
        private string Password { get; set; }
        private bool PasswordVisibility;
        private bool ShowMessage { get; set; } = false;
        private bool _processing { get; set; } = false;
        private string Message { get; set; }
        private Severity Severity { get; set; }

        InputType PasswordInput = InputType.Password;
        string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }
        private User utilisateur { get; set; } = new User();
        void TogglePasswordVisibility()
        {
            if (PasswordVisibility)
            {
                PasswordVisibility = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
            }
            else
            {
                PasswordVisibility = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }
        private void CloseMe(bool value)
        {
            if (value)
            {
                ShowMessage = false;
            }
            else
            {
                ShowMessage = false;
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            var user = (await authenticationStateTask).User.Identity;
            if (user != null)
            {
                if (user.IsAuthenticated)
                {
                    navigationManager.NavigateTo("/");
                }
            }
        }
        private async Task LoginFunction()
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                Severity = Severity.Error;
                ShowMessage = true;
                Message = "Veuillez renseigner correctement les champs.";
                snackBar.Add(Message, Severity);
                return;
            }
            else
            {
                _processing = true;
                utilisateur = await usersInterface.Login(Email, Password);
                if (utilisateur != null && utilisateur.Id != 0)
                {
                    ((StateProviderAuthentication)AuthenticationStateProvider).MarkUserAsAuthenticated(utilisateur);
                    navigationManager.NavigateTo("/");
                }
                else
                {
                    Severity = Severity.Error;
                    ShowMessage = true;
                    _processing = true;
                    Message = "Identifiants incorrectes.";
                    snackBar.Add(Message, Severity);
                    return;
                }
                _processing = false;
                StateHasChanged();
            }
        }
    }
}
