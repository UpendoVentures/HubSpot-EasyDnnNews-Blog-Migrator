using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Constants
{
    /// <summary>
    /// Constants used within the module.
    /// </summary>
    public class Constant
    {
        /// <summary>
        /// The partial root path for .resx files.
        /// </summary>
        public const string ResxPartialRoot = "~\\DesktopModules\\HubSpotEasyDnnNewsBlogMigrator\\App_LocalResources\\";

        /// <summary>
        /// The root path for .resx files.
        /// </summary>
        public const string ResxRoot = "~\\DesktopModules\\HubSpotEasyDnnNewsBlogMigrator\\App_LocalResources\\View.resx";

        /// <summary>
        /// The attribute name used in XML data nodes.
        /// </summary>
        public const string DataNodeAttributesName = "name";

        /// <summary>
        /// The path to the root data in XML.
        /// </summary>
        public const string RootData = "/root/data";

        /// <summary>
        /// The text suffix for keys ending with a period.
        /// </summary>
        public const string PointText = ".text";

        /// <summary>
        /// The name of the module folder.
        /// </summary>
        public const string ModuleFolderName = "Company.Modules.HubSpotEasyDnnNewsBlogMigrator";

        /// <summary>
        /// The name of the supported module.
        /// </summary>
        public const string SupportedModules = "HubSpotEasyDnnNewsBlogMigrator";

        /// <summary>
        /// The prefix used for database tables.
        /// </summary>
        public const string DBTABLE_PREFIX = "HubSpotEasyDnnNewsBlogMigrator";

        public const string ClientId = "ClientId";
        public const string ClientSecret = "ClientSecret";
        public const string RedirectUri = "RedirectUri";
        public const string Code = "Code";
        public const string Scope = "Scope";
        public const string FriendlyName = "HubSpotEasyDnnNewsBlogMigrator";
        public const string PhraseKey = "0123456789abcdef0123456789abcdef";
        public const string PhraseIv = "abcdef0123456789";
        public const string DefaultCategoryName = "Uncategorized";
    }
}