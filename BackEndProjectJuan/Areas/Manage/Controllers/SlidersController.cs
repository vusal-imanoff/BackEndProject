using BackEndProjectJuan.DAL;
using BackEndProjectJuan.Extensions;
using BackEndProjectJuan.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Areas.Manage.Controllers
{
    [Area("manage")]
    public class SlidersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SlidersController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sliders.ToListAsync()) ;
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return  View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if(slider.File!=null)
            {
                if (slider.File.CheckFileContentType("image/jpeg"))
                {
                    ModelState.AddModelError("File", "Please Select Correct Image Type. Must Be .jpg or .jpeg");
                    return View();
                }

                if (slider.File.CheckFileSize(50))
                {
                    ModelState.AddModelError("File", "Please Select Correct Image Size. Max 50 KB");
                    return View();
                }

                slider.Image = await slider.File.CreateFileAsync(_env, "assets", "img", "slider");
            }
            else
            {
                ModelState.AddModelError("File", "Please Select Slider Image");
                return View();
            }
            slider.MainTitle = slider.MainTitle.Trim();
            slider.SubTitle = slider.SubTitle.Trim();
            slider.Description = slider.Description.Trim();
            slider.RedirectUrl=slider.RedirectUrl.Trim();
            slider.CreatedAt = DateTime.UtcNow.AddHours(4);

            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
