using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.ViewModels
{
    public class HubspotSetting
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set; }
        public string Code { get; set; }
        public string Scope { get; set; }
    }
}