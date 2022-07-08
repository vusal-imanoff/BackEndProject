using BackEndProjectJuan.DAL;
using BackEndProjectJuan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Areas.Manage.Controllers
{   [Area("Manage")]
    public class SizeController : Controller
    {
        private readonly AppDbContext _context;
        public SizeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            //IQueryable<Brand> query = _context.Brands.Include(b => b.Products);
            //if (status != null)
            //{
            //    query = query.Where(b => b.IsDeleted == status);
            //}
            //return View(await query.ToArrayAsync());
            return View(await _context.Sizes.ToListAsync());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Size size)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (await _context.Sizes.AnyAsync(s=>s.Name.ToLower()==size.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", $"This {size.Name} Already Exists");
                return View();
            }
            size.Name = size.Name.Trim();
            await _context.Sizes.AddAsync(size);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id==null)
            {
                return BadRequest();
            }
            Size size =  _context.Sizes.FirstOrDefault(s => s.Id == id);
            if (size==null)
            {
                return NotFound();
            }
            return View(size);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id,Size size)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id==null)
            {
                return BadRequest();
            }
            if (size.Id!=id)
            {
                return BadRequest();
            }
            Size dbsize = await _context.Sizes.FirstOrDefaultAsync(s => s.Id == size.Id);
            if (dbsize==null)
            {
                return NotFound();
            }
            if (await _context.Sizes.AnyAsync(s => s.Id != size.Id && s.Name.ToLower() == size.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", $"This {size.Name} Already Exists");
                return View();
            }
            if (dbsize.Name.ToLower()==size.Name.Trim().ToLower())
            {
                RedirectToAction("index");
            }
            dbsize.Name = size.Name.Trim();
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Size size = await _context.Sizes.FirstOrDefaultAsync(s => !s.IsDeleted && s.Id == id);
            if (size == null)
            {
                return NotFound();
            }
            size.IsDeleted = true;
            await _context.SaveChangesAsync();
            return PartialView("_SizeIndexPartial", await _context.Sizes.ToListAsync());

        }
        public async Task<IActionResult> Restore(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Size size = await _context.Sizes.FirstOrDefaultAsync(s => s.IsDeleted && s.Id == id);
            if (size == null)
            {
                return NotFound();
            }
            size.IsDeleted = false;
            await _context.SaveChangesAsync();
            return PartialView("_SizeIndexPartial", await _context.Sizes.ToListAsync());

        }
    }
}
