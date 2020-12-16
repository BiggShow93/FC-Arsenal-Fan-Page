namespace ArsenalFanPage.Web.ViewModels.News
{
    using System;

    using ArsenalFanPage.Data.Models;
    using ArsenalFanPage.Services.Mapping;
    using Ganss.XSS;

    public class NewsCommentViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? ParentId { get; set; }

        public string Content { get; set; }

        public string SanitizedContent => new HtmlSanitizer().Sanitize(this.Content);

        public string UserUserName { get; set; }
    }
}
