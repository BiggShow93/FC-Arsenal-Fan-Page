namespace ArsenalFanPage.Web.Controllers
{
    using System.Threading.Tasks;

    using ArsenalFanPage.Data.Models;
    using ArsenalFanPage.Services.Data;
    using ArsenalFanPage.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CommentsController : BaseController
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(
            ICommentsService commentsService,
            UserManager<ApplicationUser> userManager)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CommentCreateInputModel input)
        {
            var parentId =
                input.ParentId == 0 ?
                    (int?)null :
                    input.ParentId;

            if (parentId.HasValue)
            {
                if (!this.commentsService.IsInNewsId(parentId.Value, input.NewsId))
                {
                    return this.BadRequest();
                }
            }

            var userId = this.userManager.GetUserId(this.User);
            await this.commentsService.Create(input.NewsId, userId, input.Content, parentId);
            return this.RedirectToAction("ById", "Posts", new { id = input.NewsId });
        }
    }
}
