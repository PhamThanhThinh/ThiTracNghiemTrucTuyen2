using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;
using ThiTracNghiemTrucTuyen.Web;
using ThiTracNghiemTrucTuyen.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

static void ConfigureRefit(IServiceCollection services)
{
  const string ApiUrl = "https://localhost:7274";
  services.AddRefitClient<IAuthApi>().ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(ApiUrl));
}