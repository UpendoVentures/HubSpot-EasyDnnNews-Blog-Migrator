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
        private readonly ModuleController _moduleController;
        private readonly int _portalId;
        private readonly ModuleInfo _module;
        private readonly UserInfo _currentUser;
        private readonly IEncryptionHelper _encryptionHelper;
        private readonly string ResourceFile = Constant.ResxRoot;

        public EasyDNNNewsCategoryListRepository(DapperContext context, IEncryptionHelper encryptionHelper) : base(context)
        {
            _connection = context.CreateConnection();
            _logger = LoggerSource.Instance.GetLogger(GetType());
            _moduleController = new ModuleController();
            _portalId = PortalController.Instance.GetCurrentPortalSettings().PortalId;
            _module = _moduleController.GetModuleByDefinition(_portalId, Constant.FriendlyName);
            _currentUser = UserController.Instance.GetCurrentUserInfo();
            _encryptionHelper = encryptionHelper;
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
