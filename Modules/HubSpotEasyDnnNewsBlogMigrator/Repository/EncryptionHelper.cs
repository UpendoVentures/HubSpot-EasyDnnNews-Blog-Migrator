﻿using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository.Contract;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Constants;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Services
{
    public class EncryptionHelper: IEncryptionHelper
    {
        public string EncryptString(string item)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(Constant.PhraseKey);
            byte[] ivBytes = Encoding.UTF8.GetBytes(Constant.PhraseIv);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = ivBytes;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(item);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        public string DecryptString(string item)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(Constant.PhraseKey);
            byte[] ivBytes = Encoding.UTF8.GetBytes(Constant.PhraseIv);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = ivBytes;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(item)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}