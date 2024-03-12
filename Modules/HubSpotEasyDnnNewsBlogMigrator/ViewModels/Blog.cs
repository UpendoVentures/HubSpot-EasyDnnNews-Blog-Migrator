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

using System.Collections.Generic;

namespace UpendoVentures.Modules.HubSpotEasyDnnNewsBlogMigrator.ViewModels
{
    public class Blog
    {
        public long ArchivedAt { get; set; }
        public bool ArchivedInDashboard { get; set; }
        public List<string> AttachedStylesheets { get; set; }
        public string AuthorName { get; set; }
        public string BlogAuthorId { get; set; }
        public int CategoryId { get; set; }
        public string ContentGroupId { get; set; }
        public int ContentTypeCategory { get; set; }
        public string Created { get; set; }
        public string CreatedById { get; set; }
        public string CurrentState { get; set; }
        public bool CurrentlyPublished { get; set; }
        public string Domain { get; set; }
        public bool EnableGoogleAmpOutputOverride { get; set; }
        public string FeaturedImage { get; set; }
        public string FeaturedImageAltText { get; set; }
        public string HtmlTitle { get; set; }
        public string Id { get; set; }
        public string Language { get; set; }
        public Dictionary<string, object> LayoutSections { get; set; }
        public string MetaDescription { get; set; }
        public string Name { get; set; }
        public string PostBody { get; set; }
        public string PostSummary { get; set; }
        public List<object> PublicAccessRules { get; set; }
        public bool PublicAccessRulesEnabled { get; set; }
        public string PublishDate { get; set; }
        public bool PublishImmediately { get; set; }
        public string RssBody { get; set; }
        public string RssSummary { get; set; }
        public string Slug { get; set; }
        public string State { get; set; }
        public List<long> TagIds { get; set; }
        public Dictionary<string, object> Translations { get; set; }
        public string Updated { get; set; }
        public string UpdatedById { get; set; }
        public string Url { get; set; }
        public bool UseFeaturedImage { get; set; }
        public Dictionary<string, object> WidgetContainers { get; set; }
        public Dictionary<string, object> Widgets { get; set; }
    }

}