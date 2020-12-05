namespace ArsenalFanPage.Web.Areas.Identity.Pages.Account
{
    using System.Threading.Tasks;

    using ArsenalFanPage.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<LogoutModel> logger;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger)
        {
            this.signInManager = signInManager;
            this.logger = logger;
        }

        public void OnGet()
        {
            this.signInManager.SignOutAsync();
            this.logger.LogInformation("User logged out.");
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await this.signInManager.SignOutAsync();
            ILogger<LogoutModel> logger1 = this.logger;
            logger1.LogInformation("User logged out.");

            if (returnUrl != null)
            {
                return this.LocalRedirect(returnUrl);
            }
            else
            {
                return this.RedirectToPage();
            }
        }
    }
}
