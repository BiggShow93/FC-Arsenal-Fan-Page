using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ArsenalFanPage.Web.Controllers
{
    public class NewsController : BaseController
    {
        [HttpGet("/News")]
        public IActionResult News()
        {
            return this.View();
        }
    }
}
