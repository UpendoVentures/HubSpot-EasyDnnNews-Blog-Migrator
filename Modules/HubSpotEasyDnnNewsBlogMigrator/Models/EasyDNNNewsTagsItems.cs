using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Models
{
    public class EasyDNNNewsTagsItems
    {
        public int ArticleID { get; set; }
        public long TagID { get; set; }
        public DateTime DateAdded { get; set; }
    }
}