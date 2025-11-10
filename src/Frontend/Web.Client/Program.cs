using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shared.Providers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddServiceExtensions(builder.Configuration);
await builder.Build().RunAsync();