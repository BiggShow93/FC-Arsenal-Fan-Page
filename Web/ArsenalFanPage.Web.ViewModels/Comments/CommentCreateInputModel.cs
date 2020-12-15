namespace ArsenalFanPage.Web.ViewModels.Comments
{
    public class Comment
    {
        public int NewsId { get; set; }

        public int ParentId { get; set; }

        public string Content { get; set; }
    }
}
