using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Data;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Services;
using DotNetNuke.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository.Contract;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator
{
    public class Startup : IDnnStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<DapperContext, DapperContext>();
            services.AddScoped<IHubspotRepository, HubspotRepository>();
            services.AddScoped<IEncryptionHelper, EncryptionHelper>();
        }
    }
}
