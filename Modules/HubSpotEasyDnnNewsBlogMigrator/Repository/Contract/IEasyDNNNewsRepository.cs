using System.Threading.Tasks;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Models;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository.Contract
{
    public interface IEasyDNNNewsRepository
    {
        Task<int> AddEasyDNNNews(EasyDNNNews entity);
        Task<bool> AddEasyDNNNewsCategories(EasyDNNNewsCategories entity);
        Task<bool> AddEasyDNNNewsCategoryList(EasyDNNNewsCategoryList entity);
        Task<EasyDNNNewsCategoryList> GetCategoryListByName(string categoryName);
        Task<int> MigrateImagesToEasyDNNNews(string originFolderPath);
        Task<bool> CopyImageToFolderAsync(string sourcePath, string articleImage, string destinationPath);
    }
}