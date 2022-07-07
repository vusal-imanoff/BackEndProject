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

        public IActionResult Index()
        {
            return View();
        }
    }
}
