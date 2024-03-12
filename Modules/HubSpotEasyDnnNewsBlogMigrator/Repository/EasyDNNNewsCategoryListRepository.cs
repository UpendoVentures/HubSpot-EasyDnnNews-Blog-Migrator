/*
Copyright Upendo Ventures, LLC 

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
DEALINGS IN THE SOFTWARE.
*/

using DotNetNuke.Entities.Controllers;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Instrumentation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Net.Http;
using System.Threading.Tasks;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Constants;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Data;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Models;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository.Contract;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.ViewModels;
using DotNetNuke.Services.Localization;
using System.Linq;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository
{
    public class EasyDNNNewsCategoryListRepository : GenericRepository<EasyDNNNewsCategoryList>, IEasyDNNNewsCategoryListRepository
    {
        private readonly IDbConnection _connection;
        private readonly ILog _logger;
     
        public EasyDNNNewsCategoryListRepository(DapperContext context) : base(context)
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
        public async Task<int> AddEasyDNNNewsCategoryList(EasyDNNNewsCategoryList entity)
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
        /// Retrieves a list of EasyDNNNewsCategoryList by category name.
        /// </summary>
        /// <param name="categoryName">The name of the category.</param>
        /// <returns>A Task that represents the asynchronous operation. The Task result contains a list of EasyDNNNewsCategoryList.</returns>
        public async Task<EasyDNNNewsCategoryList> GetCategoryListByName(string categoryName)
        {
            EasyDNNNewsCategoryList result = null;
            try
            {
                string tableName = GetSingleTableName();
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
