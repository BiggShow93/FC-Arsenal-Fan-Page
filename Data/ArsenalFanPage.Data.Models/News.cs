namespace ArsenalFanPage.Data.Models
{
    using System.Collections.Generic;

    using ArsenalFanPage.Data.Common.Models;

    public class News : BaseDeletableModel<int>
    {
        public News()
        {
            this.Images = new HashSet<Image>();
        }

        public string GeneralTitle { get; set; }

        public string DescriptionTitle { get; set; }

        public string Description { get; set; }

        public string CreatedByUserId { get; set; }

        public virtual ApplicationUser CreatedByUser { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
