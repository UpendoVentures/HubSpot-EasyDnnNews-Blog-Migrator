using DotNetNuke.Entities.Controllers;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Instrumentation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Net.Http;
using System.Threading.Tasks;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Constants;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Data;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Models;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository.Contract;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.ViewModels;
using DotNetNuke.Services.Localization;
using System.Linq;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository
{
    public class HubspotRepository : GenericRepository<EasyDNNNews>, IHubspotRepository
    {
        private readonly IDbConnection _connection;
        private readonly ILog _logger;
        private readonly ModuleController _moduleController;
        private readonly int _portalId;
        private readonly ModuleInfo _module;
        private readonly int _moduleId;
        private readonly UserInfo _currentUser;
        private readonly IEncryptionHelper _encryptionHelper;
        private readonly IEasyDNNNewsRepository _easyDNNNewsRepository;
        private readonly IEasyDNNNewsCategoriesRepository _easyDNNNewsCategoriesRepository;
        private readonly IEasyDNNNewsCategoryListRepository _easyDNNNewsCategoryListRepository;
        private readonly string ResourceFile = Constant.ResxPartialRoot;

        public HubspotRepository(DapperContext context, IEasyDNNNewsRepository easyDNNNewsRepository,
            IEasyDNNNewsCategoriesRepository easyDNNNewsCategoriesRepository, IEasyDNNNewsCategoryListRepository easyDNNNewsCategoryListRepository, IEncryptionHelper encryptionHelper) : base(context)
        {
            _connection = context.CreateConnection();
            _logger = LoggerSource.Instance.GetLogger(GetType());
            _moduleController = new ModuleController();
            _portalId = PortalController.Instance.GetCurrentPortalSettings().PortalId;
            _module = _moduleController.GetModuleByDefinition(_portalId, Constant.FriendlyName);
            _moduleId = _module.ModuleID;
            _currentUser = UserController.Instance.GetCurrentUserInfo();
            _encryptionHelper = encryptionHelper;
            _easyDNNNewsRepository = easyDNNNewsRepository;
            _easyDNNNewsCategoriesRepository = easyDNNNewsCategoriesRepository;
            _easyDNNNewsCategoryListRepository = easyDNNNewsCategoryListRepository;
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

        public new async Task<string> MigratePosts(string accessToken)
        {
            var result = "";
            try
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, "https://api.hubapi.com/cms/v3/blogs/posts");
                    request.Headers.Add("Authorization", $"Bearer {accessToken}");
                    var response = await client.SendAsync(request);
                    var content = await response.Content.ReadAsStringAsync();
                    var blogResponse = JsonConvert.DeserializeObject<BlogResponse>(content);
                    if (blogResponse.Category == "EXPIRED_AUTHENTICATION")
                    {
                        return blogResponse.Message;
                    }
                    var blogs = blogResponse.Results;
                    var rowsEffected = 0;
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
                                SubTitle = "",
                                Summary = item.PostSummary,
                                Article = item.PostBody,
                                ArticleImage = item.FeaturedImage,
                                DateAdded = DateTime.Parse(item.Created, null, System.Globalization.DateTimeStyles.RoundtripKind),
                                LastModified = DateTime.Parse(item.Updated, null, System.Globalization.DateTimeStyles.RoundtripKind),
                                PublishDate = DateTime.Parse(item.PublishDate, null, System.Globalization.DateTimeStyles.RoundtripKind),
                                ExpireDate = DateTime.Parse(item.PublishDate, null, System.Globalization.DateTimeStyles.RoundtripKind).AddYears(1),
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
                                ArticleFromRSS = false,
                                HasPermissions = false,
                                EventArticle = false,
                                ShowGallery = true,
                                HideDefaultLocale = false,
                                Featured = false,
                                Approved = true,
                                AllowComments = true,
                                Active = true,
                                ShowMainImage = true,
                                ShowMainImageFront = true,
                                ArticleImageSet = true,
                                GoodVotesCount = 0,
                                BadVotesCount = 0,
                                Published = false,
                                WorkflowId = 1,
                                RevisionHistoryEntryID = 0,
                                DetailMediaType = "Image",
                                DetailsArticleImage = "",
                                OpenGraphMetaTags = "",
                                TwitterCardMetaTags = "",
                                StructuredDataJSON = "",
                                ArticleGalleryID = null,
                                CFGroupeID = null,
                                DetailsDocumentsTemplate = null,
                                DetailsLinksTemplate = null,
                                DetailsRelatedArticlesTemplate = null,
                                ContactEmail = null,
                                TitleTag = null,
                                SimpleForumTopicId = null,
                                AddRobotsFollowTag = null
                            };
                            easyDNNNews.ArticleID = await _easyDNNNewsRepository.AddEasyDNNNews(easyDNNNews);
                            if (easyDNNNews.ArticleID != 0)
                            {
                                rowsEffected = +1;
                            }
                            var easyDNNNewsCategoryList = await _easyDNNNewsCategoryListRepository.GetCategoryListByName(Constant.DefaultCategoryName);

                            var newEasyDNNNewsCategoryList = new EasyDNNNewsCategoryList();
                            if (easyDNNNewsCategoryList == null)
                            {
                                newEasyDNNNewsCategoryList = new EasyDNNNewsCategoryList
                                {
                                    CategoryName = Constant.DefaultCategoryName,
                                    PortalID = _portalId,
                                    Position = 1,
                                    ParentCategory = 0,
                                    Level = 0,
                                    CategoryURL = null,
                                    CategoryImage = null,
                                    CategoryText = null,
                                    Color = "default",
                                    SearchableText = Constant.DefaultCategoryName,
                                    QueryLink = null,
                                    TitleTag = null,
                                    MetaDecription = null,
                                    MetaKeywords = null,
                                    AddRobotsFollowTag = null,
                                };
                                newEasyDNNNewsCategoryList.CategoryID = await _easyDNNNewsCategoryListRepository.AddEasyDNNNewsCategoryList(newEasyDNNNewsCategoryList);
                            }
                            if (easyDNNNews.ArticleID!=0)
                            {
                                var easyDNNNewsCategories = new EasyDNNNewsCategories()
                                {
                                    ArticleID = easyDNNNews.ArticleID,
                                    CategoryID = easyDNNNewsCategoryList == null ? newEasyDNNNewsCategoryList.CategoryID : easyDNNNewsCategoryList.CategoryID,
                                };
                                var categories = await _easyDNNNewsCategoriesRepository.AddEasyDNNNewsCategories(easyDNNNewsCategories);
                            }

                            if (rowsEffected == 0)
                            {
                                return $"{Localization.GetString("ATotalOf.Text", ResourceFile)} 0 {Localization.GetString("WereMigratedSuccessfully.Text", ResourceFile)}";
                            }
                        }
                        return $"{Localization.GetString("ATotalOf.Text", ResourceFile)} {blogResponse.Total} {Localization.GetString("WereMigratedSuccessfully.Text", ResourceFile)}";
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error making the HTTP request", ex);
            }
            return result;
        }
    }
}
