using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using study_abroad;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://api.postmarkapp.com/")
});

// ✅ Read the Postmark API key from appsettings.json
var apiKey = builder.Configuration["Postmark:ApiKey"];

if (string.IsNullOrWhiteSpace(apiKey))
{
    throw new Exception("Postmark API key is missing. Please set it in wwwroot/appsettings.json.");
}

builder.Services.AddSingleton(new ContactUsService(apiKey));

await builder.Build().RunAsync();