using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Models
{
    public class EasyDNNNewsNewTags
    {
        [Key]
        public int TagID { get; set; }
        public string Name { get; set; }
        public int PortalID { get; set; }
        public DateTime DateCreated { get; set; }
    }
}