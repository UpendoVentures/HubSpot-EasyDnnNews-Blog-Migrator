using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Instrumentation;
using System;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Constants;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Data;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Models;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository.Contract;
using System.Linq;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository
{
    public class EasyDNNNewsRepository : GenericRepository<EasyDNNNews>, IEasyDNNNewsRepository
    {
        private readonly IDbConnection _connection;
        private readonly ILog _logger;

        public EasyDNNNewsRepository(DapperContext context) : base(context)
        {
            _connection = context.CreateConnection();
            _logger = LoggerSource.Instance.GetLogger(GetType());
       }

        /// <summary>
        /// Adds a new entity of type T to the database.
        /// </summary>
        /// <param name="entity">The entity to add to the database.</param>
        /// <returns>
        ///   <c>true</c> if the entity was successfully added; otherwise, <c>false</c>.
        /// </returns>
        public async Task<int> AddEasyDNNNews(EasyDNNNews entity)
        {
            int insertedId = 0;
            try
            {
                // Get the name of the table associated with the entity type.
                string tableName = GetSingleTableName();

                // Get the names of columns and properties, excluding the primary key.
                string columns = GetColumns(excludeKey: true);
                string properties = GetPropertyNames(excludeKey: true);

                // Create an SQL query to insert the entity into the table and return the ID.
                string query = $"INSERT INTO {tableName} ({columns}) VALUES ({properties}); SELECT CAST(SCOPE_IDENTITY() as int)";

                // Execute the insert query asynchronously, specifying the entity as parameters.
                insertedId = await _connection.QuerySingleAsync<int>(query, entity);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            // Return the ID of the inserted row.
            return insertedId;
        }

        /// <summary>
        /// Retrieves a list of EasyDNNNews by title.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> AddEasyDNNNewsCategories(EasyDNNNewsCategories entity)
        {
            int rowsEffected = 0;
            try
            {
                // Get the name of the table associated with the entity type.
                string tableName = GetSingleTableName();

                // Get the names of columns and properties, excluding the primary key.
                string columns = GetColumns(excludeKey: true);
                string properties = GetPropertyNames(excludeKey: true);

                // Create an SQL query to insert the entity into the table.
                string query = $"INSERT INTO {tableName} ({columns}) VALUES ({properties})";

                // Execute the insert query asynchronously, specifying the entity as parameters.
                rowsEffected = await _connection.ExecuteAsync(query, entity);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            // Return true if at least one row is affected by the insert; otherwise, return false.
            return rowsEffected > 0;
        }
        
        /// <summary>
        /// Retrieves a list of EasyDNNNewsCategories by title.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> AddEasyDNNNewsCategoryList(EasyDNNNewsCategoryList entity)
        {
            int rowsEffected = 0;
            try
            {
                // Get the name of the table associated with the entity type.
                string tableName = GetSingleTableName();

                // Get the names of columns and properties, excluding the primary key.
                string columns = GetColumns(excludeKey: true);
                string properties = GetPropertyNames(excludeKey: true);

                // Create an SQL query to insert the entity into the table.
                string query = $"INSERT INTO {tableName} ({columns}) VALUES ({properties})";

                // Execute the insert query asynchronously, specifying the entity as parameters.
                rowsEffected = await _connection.ExecuteAsync(query, entity);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            // Return true if at least one row is affected by the insert; otherwise, return false.
            return rowsEffected > 0;
        }

        /// <summary>
        /// Retrieves a list of EasyDNNNewsCategoryList by category name.
        /// </summary>
        /// <param name="categoryName">The name of the category.</param>
        /// <returns>A Task that represents the asynchronous operation. The Task result contains a list of EasyDNNNewsCategoryList.</returns>
        public async Task<EasyDNNNewsCategoryList> GetCategoryListByName(string categoryName)
        {
            EasyDNNNewsCategoryList result = null;
            try
            {
                string tableName = GetTableName();
                string query = $"SELECT * FROM {tableName} WHERE [CategoryName] = @CategoryName";
                var results = await _connection.QueryAsync<EasyDNNNewsCategoryList>(query, new { CategoryName = categoryName });

                // Get the first result or null if no results exist.
                result = results.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            // Return the first entity of type EasyDNNNewsCategoryList found in the database or null.
            return result;
        }
    }
}
