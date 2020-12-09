namespace ArsenalFanPage.Data.Models
{
    using System.Collections.Generic;

    using ArsenalFanPage.Data.Common.Models;

    public class News : BaseDeletableModel<int>
    {
        public News()
        {
            this.Comments = new HashSet<Comment>();
        }

        public string Title { get; set; }

        public string Content { get; set; }

        public string CreatedByUserId { get; set; }

        public virtual ApplicationUser CreatedByUser { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
