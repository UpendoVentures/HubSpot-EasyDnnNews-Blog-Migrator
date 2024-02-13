using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.ViewModels;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository.Contract
{
    public interface IHubspotRepository
    {
        Task<HubspotSetting> GetSettings();
        HubspotSetting UpdateSettings(HubspotSetting settings);
        Task<TokenResponse> OAuthCallback(HubspotSetting settings);
        Task<BlogResponse> GetPosts(string accessToken);
        Task<int> MigratePosts(string accessToken);
    }
}