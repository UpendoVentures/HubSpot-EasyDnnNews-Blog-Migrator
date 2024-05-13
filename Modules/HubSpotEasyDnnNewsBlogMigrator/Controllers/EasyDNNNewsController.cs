/*
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

using DotNetNuke.Web.Api;
using System.Web.Http;
using DotNetNuke.Security;
using System.Threading.Tasks;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository.Contract;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Constants;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.ViewModels;

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

        /// <summary>
        /// Remove duplicate images.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> RemoveDuplicateImages()
        {
            var result = await _easyDNNNewsRepository.RemoveDuplicateImages();
            return Ok(result);
        }

        /// <summary>
        /// Replace image urls.
        /// <paramref name="parameters"/>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> ReplaceImageUrls([FromBody] ReplaceImageUrlParams parameters)
        {
            var result = await _easyDNNNewsRepository.ReplaceImageUrls(parameters.DomainToReplace, parameters.PartialPath,parameters.SkipSegments);
            return Ok(result);
        }

        /// <summary>
        /// Replace absolute urls.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> ReplaceAbsoluteUrls([FromBody] ReplaceImageUrlParams parameters)
        {
            var result = await _easyDNNNewsRepository.ReplaceAbsoluteUrls(parameters.DomainToReplace);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IHttpActionResult> ReplaceAllSimpleUrls()
        {
            var result = await _easyDNNNewsRepository.ReplaceSimpleUrls();
            return Ok(result);
        }
    }
}