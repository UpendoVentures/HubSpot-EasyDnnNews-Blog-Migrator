﻿/*
Copyright Upendo Ventures, LLC 

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
DEALINGS IN THE SOFTWARE.
*/

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
    /// <summary>
    /// Repository for interacting with Hubspot data.
    /// </summary>
    public class HubspotRepository : GenericRepository<EasyDNNNews>, IHubspotRepository
    {
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
        private readonly IEasyDNNNewsGenericRepository<EasyDNNNewsNewTags> _tagsRepository;
        private readonly IEasyDNNNewsGenericRepository<EasyDNNNewsTagsItems> _tagsItemsRepository;

        /// <summary>
        /// Constructor for the HubspotRepository.
        /// </summary>
        /// <param name="context">The Dapper context for database operations.</param>
        /// <param name="easyDNNNewsRepository">Repository for EasyDNNNews operations.</param>
        /// <param name="easyDNNNewsCategoriesRepository">Repository for EasyDNNNews categories operations.</param>
        /// <param name="easyDNNNewsCategoryListRepository">Repository for EasyDNNNews category list operations.</param>
        /// <param name="encryptionHelper">Helper for encryption operations.</param>
        public HubspotRepository(DapperContext context, IEasyDNNNewsRepository easyDNNNewsRepository,
            IEasyDNNNewsCategoriesRepository easyDNNNewsCategoriesRepository, IEasyDNNNewsCategoryListRepository easyDNNNewsCategoryListRepository, IEncryptionHelper encryptionHelper,
            IEasyDNNNewsGenericRepository<EasyDNNNewsNewTags> tagsRepository, IEasyDNNNewsGenericRepository<EasyDNNNewsTagsItems> tagsItemsRepository) : base(context)
        {
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
            _tagsRepository = tagsRepository;
            _tagsItemsRepository = tagsItemsRepository;
        }

        /// <summary>
        /// Retrieves the Hubspot settings.
        /// </summary>
        /// <returns>The Hubspot settings.</returns>
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

        /// <summary>
        /// Updates the Hubspot settings.
        /// </summary>
        /// <param name="settings">The new Hubspot settings.</param>
        /// <returns>The updated Hubspot settings.</returns>
        public new HubspotSetting UpdateSettings(HubspotSetting settings)
        {
            var moduleController = new ModuleController();
            moduleController.UpdateModuleSetting(_moduleId, Constant.ClientId, _encryptionHelper.EncryptString(settings.ClientId.Trim()));
            moduleController.UpdateModuleSetting(_moduleId, Constant.ClientSecret, _encryptionHelper.EncryptString(settings.ClientSecret.Trim()));
            moduleController.UpdateModuleSetting(_moduleId, Constant.RedirectUri, _encryptionHelper.EncryptString(settings.RedirectUri.Trim()));
            moduleController.UpdateModuleSetting(_moduleId, Constant.Scope, _encryptionHelper.EncryptString(settings.Scope.Trim()));

            return settings;
        }

        /// <summary>
        /// Handles the OAuth callback from Hubspot.
        /// </summary>
        /// <param name="settings">The Hubspot settings.</param>
        /// <returns>The response from the OAuth callback.</returns>
        public new async Task<TokenResponse> OAuthCallback(HubspotSetting settings)
        {
            var tokenResponse = new TokenResponse { };
            try
            {
                HostController.Instance.Update(Constant.Code, settings.Code.Trim());
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.hubapi.com/oauth/v1/token")
                    {
                        Content = new FormUrlEncodedContent(new Dictionary<string, string>
                        {
                            {"grant_type", "authorization_code"},
                            {"client_id", settings.ClientId},
                            {"client_secret", settings.ClientSecret},
                            {"redirect_uri", settings.RedirectUri},
                            {"code", settings.Code}
                        })
                    };

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


        /// <summary>
        /// Retrieves the blog posts from Hubspot.
        /// </summary>
        /// <param name="accessToken">The access token for Hubspot.</param>
        /// <returns>The blog posts from Hubspot.</returns>
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

        /// <summary>
        /// Migrates the blog posts from Hubspot.
        /// </summary>
        /// <param name="accessToken">The access token for Hubspot.</param>
        /// <returns>The number of blog posts migrated.</returns>
        public async Task<int> MigratePosts(string accessToken)
        {
            var rowsEffected = 0;
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
                        return rowsEffected;
                    }
                    var blogs = blogResponse.Results;
                    try
                    {
                        // Call the base class method to perform the insert
                        foreach (var item in blogs)
                        {
                            DateTime publishDate = DateTime.Parse(item.PublishDate);

                            string filename = "";
                            try
                            {
                                string ArticleImage = item.FeaturedImage;
                                if (!string.IsNullOrEmpty(ArticleImage))
                                {
                                    Uri uri = new Uri(ArticleImage);
                                    filename = System.IO.Path.GetFileName(uri.LocalPath);
                                    filename = System.Net.WebUtility.UrlDecode(filename);
                                }
                            }
                            catch (Exception)
                            {
                                filename = "";
                            }

                            var easyDNNNews = new EasyDNNNews()
                            {
                                PortalID = _portalId,
                                UserID = _currentUser.UserID,
                                Title = item.HtmlTitle,
                                SubTitle = "",
                                Summary = item.PostSummary,
                                Article = item.PostBody,
                                ArticleImage = filename,
                                DateAdded = DateTime.Parse(item.Created, null, System.Globalization.DateTimeStyles.RoundtripKind),
                                LastModified = DateTime.Parse(item.Updated, null, System.Globalization.DateTimeStyles.RoundtripKind),
                                PublishDate = DateTime.Parse(item.PublishDate, null, System.Globalization.DateTimeStyles.RoundtripKind),
                                ExpireDate = DateTime.Parse(item.PublishDate, null, System.Globalization.DateTimeStyles.RoundtripKind).AddYears(1),
                                NumberOfViews = 0,
                                RatingValue = 0,
                                RatingCount = 0,
                                TitleLink = item.HtmlTitle.Replace(" ", "-").Replace(".", "-"),
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
                                Published = true,
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
                                rowsEffected++;
                            }

                            if (easyDNNNews.ArticleID != 0)
                            {
                                var defaultCategoryList = await AddCategoryListIfNotExit(Constant.DefaultCategoryName, 0, 0);
                                await Add_EasyDNNNewsCategories(easyDNNNews.ArticleID, defaultCategoryList.CategoryID);

                                var defaultCategoryName = $"{Constant.DefaultCategoryName}{item.CategoryId}";
                                var easyDNNNewsCategoryList = await AddCategoryListIfNotExit(defaultCategoryName, defaultCategoryList.CategoryID, 1);
                                await Add_EasyDNNNewsCategories(easyDNNNews.ArticleID, easyDNNNewsCategoryList.CategoryID);

                                if (item.TagIds.Count > 0)
                                {
                                    await HandleTags(easyDNNNews.ArticleID, item.TagIds);
                                }
                            }
                        }
                        return rowsEffected;
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
            return rowsEffected;
        }

        /// <summary>
        /// Adds the EasyDNNNewsCategoryList if it does not exist.
        /// </summary>
        /// <param name="defaultCategoryName"></param>
        /// <param name="parentCategoryId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public async Task<EasyDNNNewsCategoryList> AddCategoryListIfNotExit(string defaultCategoryName, int parentCategoryId, int level)
        {
            var easyDNNNewsCategoryList = await _easyDNNNewsCategoryListRepository.GetCategoryListByName(defaultCategoryName);

            if (easyDNNNewsCategoryList == null)
            {
                var newEasyDNNNewsCategoryList = new EasyDNNNewsCategoryList
                {
                    CategoryName = defaultCategoryName,
                    PortalID = _portalId,
                    Position = 1,
                    ParentCategory = parentCategoryId,
                    Level = level,
                    CategoryURL = null,
                    CategoryImage = null,
                    CategoryText = null,
                    Color = "default",
                    SearchableText = defaultCategoryName,
                    QueryLink = null,
                    TitleTag = null,
                    MetaDecription = null,
                    MetaKeywords = null,
                    AddRobotsFollowTag = null,
                };
                newEasyDNNNewsCategoryList.CategoryID = await _easyDNNNewsCategoryListRepository.AddEasyDNNNewsCategoryList(newEasyDNNNewsCategoryList);
                return newEasyDNNNewsCategoryList;
            }
            return easyDNNNewsCategoryList;
        }

        /// <summary>
        /// Adds the EasyDNNNewsCategories.
        /// </summary>
        /// <param name="articleID"></param>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public async Task Add_EasyDNNNewsCategories(int articleID, int categoryID)
        {
            var easyDNNNewsCategories = new EasyDNNNewsCategories()
            {
                ArticleID = articleID,
                CategoryID = categoryID,
            };
            try
            {
                await _easyDNNNewsCategoriesRepository.AddEasyDNNNewsCategories(easyDNNNewsCategories);
            }
            catch (Exception ex)
            {
                _logger.Error("Error in the request", ex);
            }
        }

        /// <summary>
        /// Handles the tags for the blog posts.
        /// </summary>
        /// <param name="articleID"></param>
        /// <param name="tagIds"></param>
        /// <returns></returns>
        public async Task HandleTags(int articleID, List<long> tagIds)
        {
            if (tagIds.Count > 0)
            {
                foreach (long tagId in tagIds)
                {
                    var name = $"hubspotTagId{tagId}";
                    var columnName = "Name";
                    var tag = await _tagsRepository.GetByNameAsync(name, columnName);

                    if (tag == null)
                    {
                        var newTag = new EasyDNNNewsNewTags
                        {
                            Name = name,
                            PortalID = _portalId,
                            DateCreated = DateTime.Now
                        };
                        var addNewTag = await _tagsRepository.AddAsync(newTag);
                        if (addNewTag)
                        {
                            var tagsItems = new EasyDNNNewsTagsItems
                            {
                                ArticleID = articleID,
                                TagID = newTag.TagID,
                                DateAdded = DateTime.Now,
                            };
                            try
                            {
                                await _tagsItemsRepository.AddAsync(tagsItems);
                            }
                            catch (Exception ex)
                            {
                                _logger.Error("Error in the request", ex);
                            }
                        }
                    }
                    else
                    {
                        var tagsItems = new EasyDNNNewsTagsItems
                        {
                            ArticleID = articleID,
                            TagID = tag.TagID,
                            DateAdded = DateTime.Now,
                        };
                        try
                        {
                            await _tagsItemsRepository.AddAsync(tagsItems);
                        }
                        catch (Exception ex)
                        {
                            _logger.Error("Error in the request", ex);
                        }
                    }
                }
            }

        }
    }
}
