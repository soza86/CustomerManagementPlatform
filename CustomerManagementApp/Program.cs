using CustomerManagementApp;
using CustomerManagementApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<CustomerService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7253/");
});
builder.Services.AddHttpClient<AutosuggestService>(client =>
{
    client.BaseAddress = new Uri("https://dev.virtualearth.net/");
});
builder.Services.AddMudServices();

await builder.Build().RunAsync();
