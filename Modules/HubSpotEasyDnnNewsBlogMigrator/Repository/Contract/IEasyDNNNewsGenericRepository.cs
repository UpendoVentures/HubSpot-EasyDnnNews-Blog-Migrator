using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository.Contract
{
    public interface IEasyDNNNewsGenericRepository<T> where T : class
    {
        Task<bool> AddAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(long Id);
        Task<T> GetByNameAsync(string Name, string ColumnName);
        Task<bool> UpdateAsync(T entity);
    }

}