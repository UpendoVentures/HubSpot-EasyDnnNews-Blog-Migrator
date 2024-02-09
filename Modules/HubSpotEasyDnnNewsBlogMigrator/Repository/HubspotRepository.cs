using DotNetNuke.Entities.Controllers;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Instrumentation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Constants;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Data;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Models;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository.Contract;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.ViewModels;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository
{
    public class HubspotRepository : GenericRepository<EasyDNNNews>, IHubspotRepository
    {
        private readonly ILog _logger;
        private readonly ModuleController _moduleController;
        private readonly int _portalId;
        private readonly ModuleInfo _module;
        private readonly int _moduleId;
        private readonly UserInfo _currentUser;
        private readonly IEncryptionHelper _encryptionHelper;

        public HubspotRepository(DapperContext context, IEncryptionHelper encryptionHelper) : base(context)
        {
            _logger = LoggerSource.Instance.GetLogger(GetType());
            _moduleController = new ModuleController();
            _portalId = PortalController.Instance.GetCurrentPortalSettings().PortalId;
            _module = _moduleController.GetModuleByDefinition(_portalId, Constant.FriendlyName);
            _moduleId = _module.ModuleID;
            _currentUser = UserController.Instance.GetCurrentUserInfo();
            _encryptionHelper = encryptionHelper;
        }

        public new async Task<HubspotSetting> GetSettings()
        {
            var setting = new HubspotSetting { };
            var moduleController = new ModuleController();
            var settings = moduleController.GetModuleSettings(_moduleId);

            if (settings != null)
            {
                setting.ClientId = settings.ContainsKey(Constant.ClientId) ? _encryptionHelper.DecryptString(settings[Constant.ClientId] as string) : null;
                setting.ClientSecret = settings.ContainsKey(Constant.ClientSecret) ? _encryptionHelper.DecryptString(settings[Constant.ClientSecret] as string) : null;
                setting.RedirectUri = settings.ContainsKey(Constant.RedirectUri) ? _encryptionHelper.DecryptString(settings[Constant.RedirectUri] as string) : null;
                setting.Scope = settings.ContainsKey(Constant.Scope) ? _encryptionHelper.DecryptString(settings[Constant.Scope] as string) : null;
            }
            return setting;
        }



        public new async Task<HubspotSetting> UpdateSettings(HubspotSetting settings)
        {
            var moduleController = new ModuleController();
            moduleController.UpdateModuleSetting(_moduleId, Constant.ClientId, _encryptionHelper.EncryptString(settings.ClientId.Trim()));
            moduleController.UpdateModuleSetting(_moduleId, Constant.ClientSecret, _encryptionHelper.EncryptString(settings.ClientSecret.Trim()));
            moduleController.UpdateModuleSetting(_moduleId, Constant.RedirectUri, _encryptionHelper.EncryptString(settings.RedirectUri.Trim()));
            moduleController.UpdateModuleSetting(_moduleId, Constant.Scope, _encryptionHelper.EncryptString(settings.Scope.Trim()));

            return settings;
        }

        public new async Task<TokenResponse> OAuthCallback(HubspotSetting settings)
        {
            var tokenResponse = new TokenResponse { };
            try
            {
                HostController.Instance.Update(Constant.Code, settings.Code.Trim());
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.hubapi.com/oauth/v1/token");

                    request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"grant_type", "authorization_code"},
                    {"client_id", settings.ClientId},
                    {"client_secret", settings.ClientSecret},
                    {"redirect_uri", settings.RedirectUri},
                    {"code", settings.Code}
                });

                    var response = await client.SendAsync(request);
                    var content = await response.Content.ReadAsStringAsync();
                    tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(content);
                    return tokenResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error making the HTTP request", ex);
                return tokenResponse;
            }
        }
        public new async Task<BlogResponse> GetPosts(string accessToken)
        {
            var blogResponse = new BlogResponse { };
            try
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, "https://api.hubapi.com/cms/v3/blogs/posts");
                    request.Headers.Add("Authorization", $"Bearer {accessToken}");
                    var response = await client.SendAsync(request);
                    var content = await response.Content.ReadAsStringAsync();
                    blogResponse = JsonConvert.DeserializeObject<BlogResponse>(content);
                    return blogResponse;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error making the HTTP request", ex);
                return blogResponse;
            }
        }

        public new async Task<bool> MigratePosts(string accessToken)
        {
            List<Blog> blogs = new List<Blog>();
            var rowsEffected = false;

            try
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, "https://api.hubapi.com/cms/v3/blogs/posts");
                    request.Headers.Add("Authorization", $"Bearer {accessToken}");
                    var response = await client.SendAsync(request);
                    var content = await response.Content.ReadAsStringAsync();
                    var blogResponse = JsonConvert.DeserializeObject<BlogResponse>(content);
                    blogs = blogResponse.Results;

                    try
                    {
                        // Call the base class method to perform the insert
                        foreach (var item in blogs)
                        {
                            DateTime publishDate = DateTime.Parse(item.PublishDate);
                            var easyDNNNews = new EasyDNNNews()
                            {
                                PortalID = _portalId,
                                UserID = _currentUser.UserID,
                                Title = item.HtmlTitle,
                                Summary = item.PostSummary,
                                Article = item.PostBody,
                                ArticleImage = item.FeaturedImage,
                                DateAdded = item.Created,
                                LastModified = item.Updated,
                                PublishDate = item.PublishDate,
                                ExpireDate = publishDate.AddYears(1).ToString("yyyy-MM-dd HH:mm:ss"),
                                NumberOfViews = 0,
                                RatingValue = 0,
                                RatingCount = 0,
                                TitleLink = item.HtmlTitle.Replace(" ", "-"),
                                DetailType = "Text",
                                DetailsTemplate = "DEFAULT",
                                DetailsTheme = "DEFAULT",
                                GalleryPosition = "bottom",
                                GalleryDisplayType = "chameleon",
                                CommentsTheme = "DEFAULT",
                                ArticleImageFolder = "EasyDNNNews",
                                NumberOfComments = 0,
                                MetaDecription = item.MetaDescription,
                                DisplayStyle = "DEFAULT",
                                DetailTarget = "_self",
                                CleanArticleData = item.PostBody,
                                ArticleFromRSS = item.RssSummary,
                                HasPermissions = "0",
                                EventArticle = "0",
                                DetailsArticleImage = "Image",
                                ShowGallery = "1",
                                HideDefaultLocale = "0",
                                Featured = "0",
                                Approved = "1",
                                AllowComments = "1",
                                Active = "1",
                                ShowMainImage = "1",
                                ShowMainImageFront = "1",
                                ArticleImageSet = "1",
                                GoodVotesCount = 0,
                                BadVotesCount = 0,
                                Published = "1",
                                WorkflowId = 1,
                                RevisionHistoryEntryID= 0,
                            };
                            rowsEffected = await AddAsync(easyDNNNews);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex);
                    }

                    // Return true if at least one row is affected by the insert; otherwise, return false.
                    return rowsEffected;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error making the HTTP request", ex);
            }
            return rowsEffected;
        }
    }
}
