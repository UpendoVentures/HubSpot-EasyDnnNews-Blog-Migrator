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

using DotNetNuke.Instrumentation;
using System;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Data;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Models;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository.Contract;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository
{
    /// <summary>
    /// Repository for interacting with EasyDNNNewsCategories data.
    /// </summary>
    public class EasyDNNNewsCategoriesRepository : GenericRepository<EasyDNNNewsCategories>, IEasyDNNNewsCategoriesRepository
    {
        private readonly IDbConnection _connection;
        private readonly ILog _logger;

        /// <summary>
        /// Constructor for the EasyDNNNewsCategoriesRepository.
        /// </summary>
        /// <param name="context">The Dapper context for database operations.</param>
        public EasyDNNNewsCategoriesRepository(DapperContext context) : base(context)
        {
            _connection = context.CreateConnection();
            _logger = LoggerSource.Instance.GetLogger(GetType());
        }


        /// <summary>
        /// Adds a new EasyDNNNewsCategories entity to the database.
        /// </summary>
        /// <param name="entity">The EasyDNNNewsCategories entity to add.</param>
        /// <returns>True if the entity was added successfully, false otherwise.</returns>
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
    }
}
