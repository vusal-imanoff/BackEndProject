using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
      public IActionResult Register()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Register()

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
