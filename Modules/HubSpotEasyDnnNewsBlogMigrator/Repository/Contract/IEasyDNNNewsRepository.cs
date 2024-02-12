using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Models;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.ViewModels;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository.Contract
{
    public interface IEasyDNNNewsRepository
    {
        Task<int> AddEasyDNNNews(EasyDNNNews entity);
    }
}