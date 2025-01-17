using HuffmanAlgorithm;
using HuffmanAlgorithm.Services;
using HuffmanAlgorithm.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HuffmanAlgorithm.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<HuffmanProcessingData>();


builder.Services.AddScoped<IHuffmanDecoderService, HuffmanDecoderService>();
builder.Services.AddScoped<IHuffmanDecodingService, HuffmanDecodingService>();
builder.Services.AddScoped<IHuffmanEncoderService, HuffmanEncoderService>();
builder.Services.AddScoped<IHuffmanEncodingService, HuffmanEncodingService>();
builder.Services.AddScoped<IHuffmanFileService, HuffmanFileService>();
builder.Services.AddScoped<IHuffmanGraphService, HuffmanGraphService>();
builder.Services.AddScoped<IHuffmanTreeBuilderService, HuffmanTreeBuilderService>();


await builder.Build().RunAsync();
