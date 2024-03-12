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

using System;
using System.ComponentModel.DataAnnotations;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.Models
{
    public class EasyDNNNews
    {
        [Key]
        public int ArticleID { get; set; }
        public int PortalID { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Summary { get; set; }
        public string Article { get; set; }
        public string ArticleImage { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public int NumberOfViews { get; set; }
        public decimal RatingValue { get; set; }
        public int RatingCount { get; set; }
        public string TitleLink { get; set; }
        public string DetailType { get; set; }
        public string DetailTypeData { get; set; }
        public string DetailsTemplate { get; set; }
        public string DetailsTheme { get; set; }
        public string GalleryPosition { get; set; }
        public string GalleryDisplayType { get; set; }
        public string CommentsTheme { get; set; }
        public string ArticleImageFolder { get; set; }
        public int NumberOfComments { get; set; }
        public string MetaDecription { get; set; }
        public string MetaKeywords { get; set; }
        public string DisplayStyle { get; set; }
        public string DetailTarget { get; set; }
        public string CleanArticleData { get; set; }
        public bool ArticleFromRSS { get; set; }
        public bool HasPermissions { get; set; }
        public bool EventArticle { get; set; }
        public string DetailMediaType { get; set; }
        public string DetailMediaData { get; set; }
        public string AuthorAliasName { get; set; }
        public bool ShowGallery { get; set; }
        public int? ArticleGalleryID { get; set; }
        public string MainImageTitle { get; set; }
        public string MainImageDescription { get; set; }
        public bool HideDefaultLocale { get; set; }
        public bool Featured { get; set; }
        public bool Approved { get; set; }
        public bool AllowComments { get; set; }
        public bool Active { get; set; }
        public bool ShowMainImage { get; set; }
        public bool ShowMainImageFront { get; set; }
        public bool ArticleImageSet { get; set; }
        public int? CFGroupeID { get; set; }
        public string DetailsDocumentsTemplate { get; set; }
        public string DetailsLinksTemplate { get; set; }
        public string DetailsRelatedArticlesTemplate { get; set; }
        public string ContactEmail { get; set; }
        public string TitleTag { get; set; }
        public string OpenGraphMetaTags { get; set; }
        public string TwitterCardMetaTags { get; set; }
        public string StructuredDataJSON { get; set; }
        public int GoodVotesCount { get; set; }
        public int BadVotesCount { get; set; }
        public bool Published { get; set; }
        public int WorkflowId { get; set; }
        public int RevisionHistoryEntryID { get; set; }
        public string DetailsArticleImage { get; set; }
        public int? SimpleForumTopicId { get; set; }
        public string AddRobotsFollowTag { get; set; }
    }

}