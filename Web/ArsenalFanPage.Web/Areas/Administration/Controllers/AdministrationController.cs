namespace ArsenalFanPage.Web.Areas.Administration.Controllers
{
    using ArsenalFanPage.Common;
    using ArsenalFanPage.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
