using BackEndProjectJuan.DAL;
using BackEndProjectJuan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]

    public class ColorController : Controller
    {
        private readonly AppDbContext _context;
        public ColorController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(bool? status)
        {
            IQueryable<Color> query = _context.Colors;
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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Color color)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (await _context.Colors.AnyAsync(c=>c.Name.ToLower()==color.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", $"This {color.Name} Already Exists");
                return View();
            }
            color.Name = color.Name.Trim();
            await _context.Colors.AddAsync(color);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id==null)
            {
                return BadRequest();
            }
            Color color = await _context.Colors.FirstOrDefaultAsync(c => c.Id == id);
            if (color==null)
            {
                return NotFound();
            }
            return View(color);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Color color)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id==null)
            {
                return BadRequest();
            }
            if (color==null)
            {
                return BadRequest();
            }
            Color dbcolor = await _context.Colors.FirstOrDefaultAsync(c => c.Id == color.Id);
            if (dbcolor==null)
            {
                return NotFound();
            }
            if (await _context.Colors.AnyAsync(c=>c.Id!=color.Id && c.Name.ToLower()==color.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", $"This {color.Name} Already Exists");
                return View();
            }
            if (dbcolor.Name.ToLower()==color.Name.Trim().ToLower())
            {
                RedirectToAction("index");
            }
            dbcolor.Name = color.Name.Trim();
            await _context.SaveChangesAsync();

            return RedirectToAction("index");

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id==null)
            {
                return BadRequest();
            }

            Color color = await _context.Colors.FirstOrDefaultAsync(c => !c.IsDeleted && c.Id == id);
            color.IsDeleted = true;
            await _context.SaveChangesAsync();

            return PartialView("_ColorIndexPartial", await _context.Colors.ToListAsync());
        }
        public async Task<IActionResult> Restore(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Color color = await _context.Colors.FirstOrDefaultAsync(c => c.IsDeleted && c.Id == id);
            color.IsDeleted = false;
            await _context.SaveChangesAsync();

            return PartialView("_ColorIndexPartial", await _context.Colors.ToListAsync());
        }
    }
}
