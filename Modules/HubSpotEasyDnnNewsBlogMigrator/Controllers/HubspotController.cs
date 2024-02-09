using DotNetNuke.Web.Api;
using System.Net.Http;
using System.Web.Http;
using DotNetNuke.Security;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.ViewModels;
using DotNetNuke.Services.Localization;
using System.Linq;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository.Contract;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Constants;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Services
{
    [SupportedModules(Constant.SupportedModules)]
    [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit)]
    [ValidateAntiForgeryToken]
    public class HubspotController : DnnApiController
    {
        private readonly string ResourceFile = Constant.ResxPartialRoot;
        private readonly IHubspotRepository _hubspotRepository;

        public HubspotController(IHubspotRepository hubspotRepository)
        {
            _hubspotRepository = hubspotRepository;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetSettings()
        {
            var settings = await _hubspotRepository.GetSettings();
            return Ok(settings);
        }
        [HttpPost]
        public IHttpActionResult UpdateSettings(HubspotSetting settings)
        {
            var result =  _hubspotRepository.UpdateSettings(settings);
            return Ok(result);
        }

        [HttpGet]
        public HttpResponseMessage InitiateOAuth()
        {
            var settings = _hubspotRepository.GetSettings();
            var clientId = settings.ConfigureAwait(false).GetAwaiter().GetResult().ClientId;
            var redirectUri = settings.ConfigureAwait(false).GetAwaiter().GetResult().RedirectUri;
            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(redirectUri))
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                response.Content = new StringContent(Localization.GetString("MissingConfigurationsClientIdRedirectUri.Text", ResourceFile));
                return response;
            }
            else
            {
                var authUrl = $"https://app.hubspot.com/oauth/authorize?client_id={clientId}&scope=content%20oauth&redirect_uri={redirectUri}";
                var response = Request.CreateResponse(HttpStatusCode.Redirect);
                response.Headers.Location = new Uri(authUrl);
                return response;
            }
        }

        [HttpGet]
        public async Task<IHttpActionResult> OAuthCallback(string code)
        {
            var settings = _hubspotRepository.GetSettings();
            var clientId = settings.ConfigureAwait(false).GetAwaiter().GetResult().ClientId;
            var redirectUri = settings.ConfigureAwait(false).GetAwaiter().GetResult().RedirectUri;
            var clientSecret = settings.ConfigureAwait(false).GetAwaiter().GetResult().ClientSecret;

            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(redirectUri) || string.IsNullOrEmpty(clientSecret))
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest);
                response.Content = new StringContent(Localization.GetString("MissingConfigurationsClientIdRedirectUriClientSecret.Text", ResourceFile));
                return (IHttpActionResult)response;
            }
            else
            {
                return Ok(await _hubspotRepository.OAuthCallback(new HubspotSetting { ClientId = clientId, RedirectUri = redirectUri, ClientSecret = clientSecret, Code = code }));
            }
        }

        /// <summary>
        /// Retrieves blog posts using the provided access token.
        /// </summary>
        /// <returns>A BlogResponse object containing the blog posts.</returns>
        /// <exception cref="HttpResponseException">Thrown when the access token is missing from the request headers.</exception>
        [HttpGet]
        public async Task<BlogResponse> GetBlogPosts()
        {
            if (Request.Headers.TryGetValues("AccessToken", out IEnumerable<string> headerValues))
            {
             var accessToken = headerValues.FirstOrDefault();
                return await _hubspotRepository.GetPosts(accessToken);
            }
            else
            {
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(Localization.GetString("MissingAccessToken.Text", ResourceFile))
                };
                throw new HttpResponseException(message);
            }
        }

        [HttpGet]
        public async Task<bool> MigratePosts()
        {
            if (Request.Headers.TryGetValues("AccessToken", out IEnumerable<string> headerValues))
            {
               var accessToken = headerValues.FirstOrDefault();
                return await _hubspotRepository.MigratePosts(accessToken);
            }
            else
            {
                var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(Localization.GetString("MissingAccessToken.Text", ResourceFile))
                };
                throw new HttpResponseException(message);
            }
        }

    }
}
