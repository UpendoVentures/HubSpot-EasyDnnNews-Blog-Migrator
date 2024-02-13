using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository.Contract
{
    public interface IEncryptionHelper
    {
        string EncryptString(string item);
        string DecryptString(string item);
    }
}