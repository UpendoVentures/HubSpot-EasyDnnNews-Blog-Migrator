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
using System.Linq;
using System.IO;
using DotNetNuke.Entities.Portals;
using System.Collections.Generic;
using System.Web.Hosting;
using DotNetNuke.Services.Localization;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Constants;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository
{
    public class EasyDNNNewsRepository : GenericRepository<EasyDNNNews>, IEasyDNNNewsRepository
    {
        private readonly IDbConnection _connection;
        private readonly ILog _logger;
        private readonly int _portalId;
        private readonly string ResourceFile = Constant.ResxRoot;

        public EasyDNNNewsRepository(DapperContext context) : base(context)
        {
            _connection = context.CreateConnection();
            _logger = LoggerSource.Instance.GetLogger(GetType());
            _portalId = PortalController.Instance.GetCurrentPortalSettings().PortalId;
        }

        /// <summary>
        /// Retrieves a list of EasyDNNNews.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EasyDNNNews>> GetAllEasyDNNNews()
        {
            IEnumerable<EasyDNNNews> result = null;
            try
            {
                // Get the name of the table associated with the entity type.
                string tableName = GetSingleTableName();

                // Construct an SQL query to select all records from the table.
                string query = $"SELECT * FROM {tableName}";

                // Execute the query asynchronously and retrieve the results into a collection.
                result = await _connection.QueryAsync<EasyDNNNews>(query);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            // Return a collection containing all entities of type T found in the database.
            return result;
        }

        /// <summary>
        /// Adds a new entity of type EasyDNNNews to the database.
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
        /// Add a EasyDNNNews entity to the database.
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
        /// Add a EasyDNNNewsCategoryList entity to the database.
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

        /// <summary>
        /// Migrate images to EasyDNNNews.
        /// </summary>
        /// <param name="originFolderPath"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<int> MigrateImagesToEasyDNNNews(string originFolderPath)
        {
            if (string.IsNullOrEmpty(originFolderPath))
            {
                throw new ArgumentException(Localization.GetString("OriginFolderPathCannotNullEmpty.Text", ResourceFile), nameof(originFolderPath));
            }

            var easyDNNNews = await GetAllEasyDNNNews() ?? new List<EasyDNNNews>();

            var result = 0;
            foreach (var item in easyDNNNews)
            {
                if (!string.IsNullOrEmpty(item.ArticleImage))
                {
                    try
                    {
                        var destinationFolderPath = Path.Combine(Path.Combine(HostingEnvironment.MapPath("~"), "Portals", _portalId.ToString(), "EasyDNNNews", item.ArticleID.ToString()));
                        var copyImage = await CopyImageToFolderAsync(originFolderPath, item.ArticleImage, destinationFolderPath);
                       
                        if (copyImage)
                        {
                            result++;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex);
                        throw;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Find and copy the image to the destination folder.
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="articleImage"></param>
        /// <param name="destinationPath"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> CopyImageToFolderAsync(string sourcePath, string articleImage, string destinationPath)
        {
            var copyImage = false;

            if (string.IsNullOrEmpty(sourcePath))
            {
                throw new ArgumentException(Localization.GetString("SourcePathCannotNullEmpty.Text", ResourceFile), nameof(sourcePath));
            }

            if (string.IsNullOrEmpty(destinationPath))
            {
                throw new ArgumentException(Localization.GetString("DestinationPathCannotNullEmpty.Text", ResourceFile), nameof(destinationPath));
            }

            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            // Search for the first file in sourcePath and its subdirectories.
            var file = Directory.EnumerateFiles(sourcePath, articleImage, SearchOption.AllDirectories).FirstOrDefault();

            // If the file is found, copy it to the destination.
            if (file != null)
            {
                var destinationFilePath = Path.Combine(destinationPath, Path.GetFileName(file));
                // Check if the file already exists at the destination.
                if (!File.Exists(destinationFilePath))
                {
                    using (var sourceStream = new FileStream(file, FileMode.Open))
                    {
                        using (var destinationStream = new FileStream(destinationFilePath, FileMode.Create))
                        {
                            await sourceStream.CopyToAsync(destinationStream);
                            copyImage = true;
                        }
                    }
                }
            }
            return copyImage;
        }
    }
}
