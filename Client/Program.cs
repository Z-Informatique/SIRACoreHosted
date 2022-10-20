using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using MudBlazor.Services;
using TestCoreHosted.Client;
using TestCoreHosted.Client.Data;
using TestCoreHosted.Client.IServices;
using TestCoreHosted.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<StateProviderAuthentication>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<StateProviderAuthentication>());


builder.Services.AddScoped<IApplications, ApplicationService>();
builder.Services.AddScoped<IAppsModel, AppsModelService>();
builder.Services.AddScoped<IDatabase, DatabaseService>();
builder.Services.AddScoped<IServeur, ServeurService>();
builder.Services.AddScoped<IDb, DbService>();
builder.Services.AddScoped<IDocumentation, DocumentationService>();
builder.Services.AddScoped<IDomaine, DomaineService>();
builder.Services.AddScoped<IEnvironnement, EnvService>();
builder.Services.AddScoped<IMetier, MetierService>();
builder.Services.AddScoped<IOSystems, OSystemService>();
builder.Services.AddScoped<ITypeOS, TypeOsService>();
builder.Services.AddScoped<IVersionDb, VersionDbService>();
builder.Services.AddScoped<IBAnalytic, BAnalyticService>();
builder.Services.AddScoped<IUsers, UsersService>();
//builder.Services.AddScoped<IUploadFile, UploadFile>();
builder.Services.AddScoped<IHostingEnvironment, HostingEnvironment>();
builder.Services.AddScoped<ExportFileService>();

await builder.Build().RunAsync();
