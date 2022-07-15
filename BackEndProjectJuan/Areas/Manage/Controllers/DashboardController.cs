using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Areas.Manage.Controllers
{
    public class DashboardController : Controller
    {
        [Area("manage")]
        [Authorize(Roles = "SuperAdmin,Admin")]


        public IActionResult Index()
        {
            return View();
        }
    }
}
