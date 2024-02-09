using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Models
{
    public class EasyDNNNewsCategoryList
    {
        public int CategoryID { get; set; }
        public int PortalID { get; set; }
        public string CategoryName { get; set; }
        public int Position { get; set; }
        public int ParentCategory { get; set; }
        public int Level { get; set; }
        public string CategoryURL { get; set; }
        public string CategoryImage { get; set; }
        public string CategoryText { get; set; }
        public string Color { get; set; }
        public string SearchableText { get; set; }
        public string QueryLink { get; set; }
        public string TitleTag { get; set; }
        public string MetaDecription { get; set; }
        public string MetaKeywords { get; set; }
        public string AddRobotsFollowTag { get; set; }
    }

}