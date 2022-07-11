using BackEndProjectJuan.DAL;
using BackEndProjectJuan.Extensions;
using BackEndProjectJuan.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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
        public async Task<IActionResult> Index(bool? status)
        {
            IQueryable<Slider> query = _context.Sliders;
            if (status != null)
            {
                query = query.Where(b => b.IsDeleted == status);
            }
            ViewBag.Status = status;
            return View(await query.ToListAsync());
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

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id==null)
            {
                return BadRequest();
            }
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);

            if (slider==null)
            {
                return NotFound();
            }

            return View(slider);
        }

        [HttpPost]

        public async Task<IActionResult> Update(int? id, Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id==null)
            {
                return BadRequest();
            }
            if (id!=slider.Id)
            {
                return BadRequest();
            }

            Slider dbslider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);

            if (dbslider==null)
            {
                return NotFound();
            }

            if (slider.File != null)
            {
                if (slider.File.CheckFileContentType("image/jpeg"))
                {
                    ModelState.AddModelError("File", "Please Select Correct Image Type. Exmaple: Jpeg or Jpg");
                    return View();
                }
                if (slider.File.CheckFileSize(50))
                {
                    ModelState.AddModelError("File","Please Select Correct Image Size. Max 50 KB");
                    return View();
                }
                string fullpath = Path.Combine(_env.WebRootPath, "assets", "img", "slider", dbslider.Image);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }

                dbslider.Image = await slider.File.CreateFileAsync(_env, "assets", "img", "slider");

            }

            dbslider.MainTitle = slider.MainTitle.Trim();
            dbslider.SubTitle = slider.SubTitle.Trim();
            dbslider.Description = slider.Description.Trim();
            dbslider.RedirectUrl = slider.RedirectUrl.Trim();
            dbslider.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _context.SaveChangesAsync();

            return RedirectToAction("index");

        }

    
        public async Task<IActionResult> Delete(int? id, bool? status)
        {
            if (id == null)
            {
                return BadRequest();
            }
            //Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => !s.IsDeleted && s.Id == id);
            var slider = await _context.Set<Slider>().FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

            if (slider == null)
            {
                return NotFound();
            }
            slider.IsDeleted = true;
            slider.DeletedAt = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();

            IQueryable<Slider> query = _context.Sliders;
            if (status != null)
            {
                query.Where(s => s.IsDeleted);
            }
            ViewBag.Status = status;


            return PartialView("_SliderIndexPartial",await query.ToListAsync());
        }

        public async Task<IActionResult> Restore(int? id,bool? status)
        {
            if (id==null)
            {
                return BadRequest();
            }

            var slider = await _context.Set<Slider>().FirstOrDefaultAsync(s => s.Id == id && s.IsDeleted);
            if (slider==null)
            {
                return NotFound();
            }

            slider.IsDeleted = false;
            slider.DeletedAt = null;

            await _context.SaveChangesAsync();

            IQueryable<Slider> query = _context.Sliders;
            if (status != null)
            {
                query.Where(s => s.IsDeleted);
            }
            ViewBag.Status = status;


            return PartialView("_SliderIndexPartial", await query.ToListAsync());
        }
    }
}
