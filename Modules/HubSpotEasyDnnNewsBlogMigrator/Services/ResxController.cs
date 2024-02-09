using System;
using System.Net.Http;
using System.Web;
using DotNetNuke.Security;
using DotNetNuke.Services.Localization;
using DotNetNuke.Web.Api;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using System.Xml;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Constants;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Services
{
    /// <summary>
    /// Controller for retrieving localized resource strings.
    /// </summary>
    [SupportedModules("HubSpotEasyDnnNewsBlogMigrator")]
    [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
    public class ResxController : DnnApiController
    {
        /// <summary>
        /// Retrieves localized resource strings from a .resx file.
        /// </summary>
        /// <param name="filename">The name of the .resx file (without the .resx extension).</param>
        /// <returns>An HTTP response containing the localized resource strings in JSON format.</returns>
        [HttpGet]
        [ActionName("GetResx")]
        public HttpResponseMessage GetResx(string filename)
        {
            var resx = new JObject();

            var resxRoot = $"{Constant.ResxPartialRoot}{filename}.resx";
            var filepath = HttpContext.Current.Server.MapPath(resxRoot);
            var resxDoc = new XmlDocument();
            resxDoc.Load(filepath);

            foreach (XmlNode dataNode in resxDoc.DocumentElement.SelectNodes(Constant.RootData))
            {
                var key = dataNode.Attributes[Constant.DataNodeAttributesName].Value;
                var val = Localization.GetString(key.ToString(), resxRoot);

                if (key.EndsWith(Constant.PointText, StringComparison.InvariantCultureIgnoreCase)) key = key.Substring(0, key.Length - 5);
                key = key.Replace(".", "_");

                resx.Add(key, val);
            }

            return Request.CreateResponse(resx);
        }
    }
}
