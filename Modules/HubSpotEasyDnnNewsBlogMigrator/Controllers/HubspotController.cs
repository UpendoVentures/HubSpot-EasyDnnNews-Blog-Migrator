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
    /// <summary>
    /// Controller for handling HubSpot related operations.
    /// </summary>
    [SupportedModules(Constant.SupportedModules)]
    [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit)]
    [ValidateAntiForgeryToken]
    public class HubspotController : DnnApiController
    {
        private readonly string ResourceFile = Constant.ResxRoot;
        private readonly IHubspotRepository _hubspotRepository;

        /// <summary>
        /// Constructor for HubspotController.
        /// </summary>
        /// <param name="hubspotRepository">The repository to use for HubSpot related operations.</param>
        public HubspotController(IHubspotRepository hubspotRepository)
        {
            _hubspotRepository = hubspotRepository;
        }

        /// <summary>
        /// Retrieves the current settings.
        /// </summary>
        /// <returns>The current settings.</returns>
        [HttpGet]
        public async Task<IHttpActionResult> GetSettings()
        {
            var settings = await _hubspotRepository.GetSettings();
            return Ok(settings);
        }

        /// <summary>
        /// Updates the settings.
        /// </summary>
        /// <param name="settings">The new settings to apply.</param>
        /// <returns>The result of the update operation.</returns>
        [HttpPost]
        public IHttpActionResult UpdateSettings(HubspotSetting settings)
        {
            var result = _hubspotRepository.UpdateSettings(settings);
            return Ok(result);
        }

        /// <summary>
        /// Initiates the OAuth process by returning the URL for the HubSpot OAuth endpoint.
        /// The client ID and redirect URI are retrieved from the settings.
        /// </summary>
        /// <returns>
        /// If the client ID or redirect URI are missing, returns a BadRequest result.
        /// Otherwise, returns an Ok result with the HubSpot OAuth URL.
        /// </returns>
        [HttpGet]
        public IHttpActionResult InitiateOAuth()
        {
            var settings = _hubspotRepository.GetSettings();
            var clientId = settings.ConfigureAwait(false).GetAwaiter().GetResult().ClientId;
            var redirectUri = settings.ConfigureAwait(false).GetAwaiter().GetResult().RedirectUri;
            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(redirectUri))
            {
                return BadRequest(Localization.GetString("MissingConfigurationsClientIdRedirectUri.Text", ResourceFile));
            }
            else
            {
                var authUrl = $"https://app.hubspot.com/oauth/authorize?client_id={clientId}&scope=content%20oauth&redirect_uri={redirectUri}";
                return Ok(authUrl);
            }
        }

        /// <summary>
        /// Handles the OAuth callback from HubSpot. Uses the provided code to retrieve an access token.
        /// The client ID, redirect URI, and client secret are retrieved from the settings.
        /// </summary>
        /// <param name="code">The authorization code provided by HubSpot.</param>
        /// <returns>
        /// If the client ID, redirect URI, or client secret are missing, returns a BadRequest result.
        /// Otherwise, returns an Ok result with the result of the OAuth callback operation.
        /// </returns>
        [HttpPost]
        public async Task<IHttpActionResult> OAuthCallback([FromBody] string code)
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
                var blogResponse = new BlogResponse
                {
                    Message = Localization.GetString("MissingAccessToken.Text", ResourceFile),
                    Status = "error",
                    Results = new List<Blog>(),
                    Total = 0
                };
                return blogResponse;
            }
        }

        /// <summary>
        /// Migrates posts from HubSpot to the local system.
        /// </summary>
        /// <returns>The result of the migration operation.</returns>
        [HttpGet]
        public async Task<IHttpActionResult> MigratePosts()
        {
            if (Request.Headers.TryGetValues("AccessToken", out IEnumerable<string> headerValues))
            {
                var accessToken = headerValues.FirstOrDefault();
                return Ok(await _hubspotRepository.MigratePosts(accessToken));
            }
            else
            {
                return Ok(Localization.GetString("MissingAccessToken.Text", ResourceFile));
            }
        }
    }
}
