using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Data;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Services;
using DotNetNuke.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository.Contract;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Models;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator
{
    public class Startup : IDnnStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<DapperContext, DapperContext>();
            services.AddScoped<IHubspotRepository, HubspotRepository>();
            services.AddScoped<IEasyDNNNewsRepository, EasyDNNNewsRepository>();
            services.AddScoped<IEasyDNNNewsCategoriesRepository, EasyDNNNewsCategoriesRepository>();
            services.AddScoped<IEasyDNNNewsCategoryListRepository, EasyDNNNewsCategoryListRepository>();
            services.AddScoped<IEncryptionHelper, EncryptionHelper>();
            services.AddScoped<IEasyDNNNewsGenericRepository<EasyDNNNewsNewTags>, EasyDNNNewsGenericRepository<EasyDNNNewsNewTags>>();
            services.AddScoped<IEasyDNNNewsGenericRepository<EasyDNNNewsTagsItems>, EasyDNNNewsGenericRepository<EasyDNNNewsTagsItems>>();
        }
    }
}
