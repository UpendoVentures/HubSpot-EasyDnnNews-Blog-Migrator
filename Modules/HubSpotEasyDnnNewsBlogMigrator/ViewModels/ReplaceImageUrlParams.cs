using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.ViewModels
{
    public class ReplaceImageUrlParams
    {
        public string DomainToReplace { get; set; }
        public string PartialPath { get; set; }
        public int SkipSegments { get; set; }
    }
}