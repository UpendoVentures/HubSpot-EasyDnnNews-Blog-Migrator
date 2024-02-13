using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Models
{
    public class EasyDNNNewsCategories
    {
        [Key]
        public int EntryID { get; set; }
        public int ArticleID { get; set; }
        public int CategoryID { get; set; }
    }

}