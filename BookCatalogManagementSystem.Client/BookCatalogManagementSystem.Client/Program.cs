using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BookCatalogManagementSystem.Client;
using BookCatalogManagementSystem.Client.Models.Constants;
using BookCatalogManagementSystem.Client.Services;
using BookCatalogManagementSystem.Client.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



builder.Services.AddSingleton(_ => new HubConnectionBuilder()
    .WithUrl(new Uri($"{ApiUrlAddress.ApiBaseAddress}/hubs/books"))
    .WithAutomaticReconnect()
    .Build());
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<SignalRService>();
builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});

await builder.Build().RunAsync();