using BackEndProjectJuan.DAL;
using BackEndProjectJuan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(bool? status)
        {
            IQueryable<Category> query = _context.Categories.Include(c => c.Products);
            if (status != null)
            {
                query = query.Where(c => c.IsDeleted);
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
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (await _context.Categories.AnyAsync(c => c.Name.ToLower() == category.Name.ToLower()))
            {
                ModelState.AddModelError("Name", $"This {category.Name} Already Exists");
                return View();
            }
            category.Name = category.Name.Trim();
            category.CreatedAt = DateTime.UtcNow.AddHours(4);
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id == null)
            {
                return BadRequest();
            }
            if (category == null)
            {
                return BadRequest();
            }
            Category dbcategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
            if (dbcategory == null)
            {
                return NotFound();
            }
            if (await _context.Categories.AnyAsync(c => c.Id != category.Id && c.Name.Trim() == category.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", $"This {category.Name} Already Exists.");
                return View();
            }
            if (dbcategory.Name.ToLower() == category.Name.Trim().ToLower())
            {
                RedirectToAction("index");
            }
            dbcategory.Name = category.Name.Trim();
            dbcategory.UpdatedAt = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }
        public async Task<IActionResult> Delete(int? id, bool? status)
        {
            if (id == null)
            {
                return View();
            }
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id & !c.IsDeleted);
            category.IsDeleted = true;
            category.DeletedAt = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();
            IQueryable<Category> query = _context.Categories.Include(c => c.Products);
            if (status != null)
            {
                query = query.Where(c => c.IsDeleted == status);
            }
            ViewBag.Status = status;
            return PartialView("_CategoryIndexPartial", await query.ToListAsync());
        }
        public async Task<IActionResult> Restore(int? id, bool? status)
        {
            if (id == null)
            {
                return View();
            }
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id & c.IsDeleted);
            if (category == null)
            {
                return NotFound();
            }
            category.IsDeleted = false;
            category.DeletedAt = null;
            await _context.SaveChangesAsync();
            IQueryable<Category> query = _context.Categories.Include(c => c.Products);
            if (status != null)
            {
                query = query.Where(c => c.IsDeleted == status);
            }
            ViewBag.Status = status;
            return PartialView("_CategoryIndexPartial", await query.ToListAsync());
        }
    }
}
