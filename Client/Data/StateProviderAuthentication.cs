using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using TestCoreHosted.Shared.Helpes;
using TestCoreHosted.Shared.Models;

namespace TestCoreHosted.Client.Data
{
    public class StateProviderAuthentication : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private static string Role = string.Empty;
        public StateProviderAuthentication(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var Nom = await _localStorageService.GetItemAsync<string>("nom");
                var Prenom = await _localStorageService.GetItemAsync<string>("prenom");
                var role = await _localStorageService.GetItemAsync<string>("role");

                ClaimsIdentity identity;

                if (!string.IsNullOrEmpty(Nom) && !string.IsNullOrEmpty(role))
                {
                    identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, Nom +" "+ Prenom),
                        new Claim(ClaimTypes.Role, Convert.ToString(role))
                    }, "apiauth_type");
                }
                else
                {
                    identity = new ClaimsIdentity();
                }

                var claimsPrincipal = new ClaimsPrincipal(identity);

                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch { return null; }
        }

        public async void MarkUserAsAuthenticated(User account)
        {
            if (account.Role == 0)
            {
                Role = Constantes.Admin;
            }
            else if (account.Role == 1)
            {
                Role = Constantes.SuperAdmin;
            }
            await _localStorageService.SetItemAsync("id", account.Id);
            await _localStorageService.SetItemAsync("token", account.Token);
            await _localStorageService.SetItemAsync("refreshToken", account.ExpireToken);
            await _localStorageService.SetItemAsync("nom", account.Nom);
            await _localStorageService.SetItemAsync("prenom", account.Prenom);
            await _localStorageService.SetItemAsync("telephone", account.Telephone);
            await _localStorageService.SetItemAsync("email", account.Email);
            await _localStorageService.SetItemAsync("role", Role);
            await _localStorageService.SetItemAsync("idRole", account.Role);

            var tk = _localStorageService.GetItemAsync<string>("token");

            ClaimsIdentity identity;

            if (!string.IsNullOrEmpty(account.Email))
            {
                identity = GetClaimsIdentity(account);
            }
            else
            {
                identity = new ClaimsIdentity();
            }

            var claimsPrincipal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        private static ClaimsIdentity GetClaimsIdentity(User account)
        {
            var claimsIdentity = new ClaimsIdentity();
            if (account != null && !string.IsNullOrEmpty(account.Nom))
            {
                claimsIdentity = new ClaimsIdentity(new[]
                                {
                                    new Claim(ClaimTypes.Name, account.Nom + " " + account.Prenom),
                                    new Claim(ClaimTypes.Role, Role)
                                }, "apiauth_type");
            }
            return claimsIdentity;
        }

        public async Task MarkUserAsLoggOut()
        {
            await _localStorageService.RemoveItemAsync("id");
            await _localStorageService.RemoveItemAsync("refreshToken");
            await _localStorageService.RemoveItemAsync("token");
            await _localStorageService.RemoveItemAsync("nom");
            await _localStorageService.RemoveItemAsync("prenom");
            await _localStorageService.RemoveItemAsync("telephone");
            await _localStorageService.RemoveItemAsync("email");
            await _localStorageService.RemoveItemAsync("role");

            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}
