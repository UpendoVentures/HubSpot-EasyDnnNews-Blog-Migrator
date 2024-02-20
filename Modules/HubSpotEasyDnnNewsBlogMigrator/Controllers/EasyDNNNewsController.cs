using DotNetNuke.Web.Api;
using System.Web.Http;
using DotNetNuke.Security;
using System.Threading.Tasks;
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
    public class EasyDNNNewsController : DnnApiController
    {
        private readonly IEasyDNNNewsRepository _easyDNNNewsRepository;

        /// <summary>
        /// Constructor for EasyDNNNewsController.
        /// </summary>
        /// <param name="hubspotRepository">The repository to use for EasyDNNNews related operations.</param>
        public EasyDNNNewsController(IEasyDNNNewsRepository easyDNNNewsRepository)
        {
            _easyDNNNewsRepository = easyDNNNewsRepository;
        }

        /// <summary>
        /// Migrate images to EasyDNNNews.
        /// </summary>
        /// <param name="originFolderPath"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> MigrateImagesToEasyDNNNews([FromBody] string originFolderPath)
        {
            var result = await _easyDNNNewsRepository.MigrateImagesToEasyDNNNews(originFolderPath);
            return Ok(result);
        }
    }
}
