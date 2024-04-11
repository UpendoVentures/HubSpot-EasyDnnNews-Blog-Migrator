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
                key = key.Replace(Constant.Period, Constant.Underscore);

                resx.Add(key, val);
            }

            return Request.CreateResponse(resx);
        }
    }
}
