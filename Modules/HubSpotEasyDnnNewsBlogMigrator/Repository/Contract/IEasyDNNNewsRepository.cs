﻿/*
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

using System.Threading.Tasks;
using UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Models;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Repository.Contract
{
    public interface IEasyDNNNewsRepository
    {
        Task<int> AddEasyDNNNews(EasyDNNNews entity);
        Task<bool> AddEasyDNNNewsCategories(EasyDNNNewsCategories entity);
        Task<bool> AddEasyDNNNewsCategoryList(EasyDNNNewsCategoryList entity);
        Task<EasyDNNNewsCategoryList> GetCategoryListByName(string categoryName);
        Task<int> MigrateImagesToEasyDNNNews(string originFolderPath);
        Task<bool> CopyImageToFolderAsync(string sourcePath, string articleImage, string destinationPath);
        Task<int> RemoveDuplicateImages();
        Task<int> ReplaceImageUrls(string domainToReplace, string partialPath, int skipSegments);
        Task<int> ReplaceAbsoluteUrls(string domainToReplace);
        Task<int> ReplaceSimpleUrls();
    }
}