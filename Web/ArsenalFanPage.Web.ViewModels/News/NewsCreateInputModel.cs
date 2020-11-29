namespace ArsenalFanPage.Web.ViewModels.News
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class NewsCreateInputModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropDownViewModel> Categories { get; set; }

        // TODO: Upload Image
    }
}
