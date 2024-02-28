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