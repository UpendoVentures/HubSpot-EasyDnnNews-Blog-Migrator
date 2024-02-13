using DotNetNuke.Web.Api;
using System.Web.Http;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Services
{
    /// <summary>
    /// The ServiceRouteMapper tells the DNN Web API Framework what routes this module uses
    /// </summary>
    public class ServiceRouteMapper : IServiceRouteMapper
    {
         /// <summary>
        /// RegisterRoutes is used to register the module's routes
        /// </summary>
        /// <param name="mapRouteManager"></param>
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute(
                moduleFolderName: "UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator",
                routeName: "default",
                url: "{controller}/{action}",
                namespaces: new[] { "UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Services" });
        }
    }
}