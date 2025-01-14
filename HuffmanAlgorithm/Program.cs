using HuffmanAlgorithm;
using HuffmanAlgorithm.Services;
using HuffmanAlgorithm.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IHuffmanEncoderService, HuffmanEncoderService>();
builder.Services.AddScoped<IHuffmanFileService, HuffmanFileService>();

await builder.Build().RunAsync();
