using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.ViewModels
{
    public class BlogResponse
    {
        public int Total { get; set; }
        public List<Blog> Results { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }
        public string CorrelationId { get; set; }
        public Context Context { get; set; }
    }
    public class Context
    {
        [JsonProperty("expire time")]
        public List<string> ExpireTime { get; set; }
        public List<string> FormattedExpireTimes
        {
            get
            {
                var formattedExpireTimes = new List<string>();
                if (ExpireTime != null && ExpireTime.Count > 0)
                {
                    foreach (var expireTime in ExpireTime)
                    {
                        if (DateTime.TryParse(expireTime, out DateTime date))
                        {
                            formattedExpireTimes.Add(date.ToString("yyyy-MM-dd HH:mm"));
                        }
                    }
                }
                return formattedExpireTimes;
            }
        }
    }
}