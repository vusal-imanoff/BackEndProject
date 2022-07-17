using BackEndProjectJuan.DAL;
using BackEndProjectJuan.Extensions;
using BackEndProjectJuan.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]

    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BlogController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        [HttpGet]
        public async Task<IActionResult> Index(bool? status)
        {
            IQueryable<Blog> query = _context.Blogs;
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
        public async Task<IActionResult> Create(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (await _context.Blogs.AnyAsync(b=>b.Name.ToLower()==blog.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", $"This {blog.Name} Already Exists.");
                return View();
            }
            if (blog.File != null)
            {
                if (blog.File.CheckFileContentType("image/jpeg"))
                {
                    ModelState.AddModelError("File", "Please Select Correct Image Type. Must Be .jpg or .jpeg");
                    return View();
                }

                if (blog.File.CheckFileSize(50))
                {
                    ModelState.AddModelError("File", "Please Select Correct Image Size. Max 50 KB");
                    return View();
                }

                blog.Image = await blog.File.CreateFileAsync(_env, "assets", "img", "blog");
            }
            else
            {
                ModelState.AddModelError("File", "Please Select Blog Image");
                return View();
            }
            blog.Name = blog.Name.Trim();
            blog.Blacknote = blog.Blacknote.Trim();
            blog.MainDescription = blog.MainDescription.Trim();
            blog.SubDescription = blog.SubDescription.Trim();
            blog.CreatedAt = DateTime.UtcNow.AddHours(4);

            await _context.Blogs.AddAsync(blog);
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
            Blog blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id);
            if (blog==null)
            {
                return NotFound();
            }
            return View(blog);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id==null)
            {
                return BadRequest();
            }
            if (id!=blog.Id)
            {
                return BadRequest();
            }

            Blog dbblog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
            if (blog==null)
            {
                return NotFound();
            }
            if (await _context.Brands.AnyAsync(b => b.Id != blog.Id && b.Name.ToLower() == blog.Name.Trim().ToLower()))
            {
                ModelState.AddModelError("Name", $"This {blog.Name} Already Exists");
                return View();
            }

            if (dbblog.Name.ToLower() == blog.Name.Trim().ToLower())
            {
                RedirectToAction("index");
            }
            if (blog.File != null)
            {
                if (blog.File.CheckFileContentType("image/jpeg"))
                {
                    ModelState.AddModelError("File", "Please Select Correct Image Type. Must Be .jpg or .jpeg");
                    return View();
                }

                if (blog.File.CheckFileSize(50))
                {
                    ModelState.AddModelError("File", "Please Select Correct Image Size. Max 50 KB");
                    return View();
                }
                string fullpath = Path.Combine(_env.WebRootPath, "assets", "img", "blog", dbblog.Image);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }

                dbblog.Image = await blog.File.CreateFileAsync(_env, "assets", "img", "blog");
            }

            dbblog.Name = blog.Name.Trim();
            dbblog.MainDescription = blog.MainDescription.Trim();
            dbblog.SubDescription = blog.SubDescription.Trim();
            dbblog.Blacknote = blog.Blacknote.Trim();
            blog.UpdatedAt = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();

            return RedirectToAction("index");
        }

        public async Task<IActionResult> Delete(int? id, bool? status)
        {
            if (id==null)
            {
                return BadRequest();
            }
            Blog blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
            if (blog==null)
            {
                return NotFound();
            }
            blog.IsDeleted = true;
            blog.DeletedAt = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();


            IQueryable<Blog> query = _context.Blogs;
            if (status != null)
            {
                query = query.Where(b => b.IsDeleted == status);
            }
            ViewBag.Status = status;
            return PartialView("_BlogIndexPartial",await query.ToListAsync());
        }
        public async Task<IActionResult> Restore(int? id, bool? status)
        {
            if (id==null)
            {
                return BadRequest();
            }
            Blog blog = await _context.Blogs.FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted);
            if (blog==null)
            {
                return NotFound();
            }
            blog.IsDeleted = false;
            blog.DeletedAt = null;
            await _context.SaveChangesAsync();


            IQueryable<Blog> query = _context.Blogs;
            if (status != null)
            {
                query = query.Where(b => b.IsDeleted == status);
            }
            ViewBag.Status = status;
            return PartialView("_BlogIndexPartial",await query.ToListAsync());
        }
    }
}
