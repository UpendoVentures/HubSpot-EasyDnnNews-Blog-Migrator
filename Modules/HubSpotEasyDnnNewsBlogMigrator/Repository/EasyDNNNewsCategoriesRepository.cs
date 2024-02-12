﻿using DotNetNuke.Entities.Modules;
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

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository
{
    public class EasyDNNNewsCategoriesRepository : GenericRepository<EasyDNNNewsCategories>, IEasyDNNNewsCategoriesRepository
    {
        private readonly IDbConnection _connection;
        private readonly ILog _logger;
        private readonly ModuleController _moduleController;
        private readonly int _portalId;
        private readonly ModuleInfo _module;
        private readonly int _moduleId;
        private readonly UserInfo _currentUser;
        private readonly IEncryptionHelper _encryptionHelper;
        private readonly string ResourceFile = Constant.ResxPartialRoot;

        public EasyDNNNewsCategoriesRepository(DapperContext context, IEncryptionHelper encryptionHelper) : base(context)
        {
            _connection = context.CreateConnection();
            _logger = LoggerSource.Instance.GetLogger(GetType());
            _moduleController = new ModuleController();
            _portalId = PortalController.Instance.GetCurrentPortalSettings().PortalId;
            _module = _moduleController.GetModuleByDefinition(_portalId, Constant.FriendlyName);
            _moduleId = _module.ModuleID;
            _currentUser = UserController.Instance.GetCurrentUserInfo();
            _encryptionHelper = encryptionHelper;
        }

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
            return rowsEffected > 0 ? true : false;
        }
    }
}