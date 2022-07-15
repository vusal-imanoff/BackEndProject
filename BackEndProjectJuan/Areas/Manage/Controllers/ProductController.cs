using BackEndProjectJuan.DAL;
using BackEndProjectJuan.Extensions;
using BackEndProjectJuan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    [Authorize(Roles = "SuperAdmin,Admin")]

    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(bool? status)
        {
            IQueryable<Product> query = _context.Products;
            if (status != null)
            {
                query = query.Where(p => p.IsDeleted == status);
            }
            ViewBag.Status = status;
            return View(await query.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Brands = await _context.Brands.Where(b => !b.IsDeleted).ToListAsync();
            ViewBag.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            ViewBag.Genders = await _context.Genders.Where(g => !g.IsDeleted).ToListAsync();
            ViewBag.Tags = await _context.Tags.Where(t => !t.IsDeleted).ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            ViewBag.Brands = await _context.Brands.Where(b => !b.IsDeleted).ToListAsync();
            ViewBag.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            ViewBag.Genders = await _context.Genders.Where(g => !g.IsDeleted).ToListAsync();
            ViewBag.Tags = await _context.Tags.Where(t => !t.IsDeleted).ToListAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!await _context.Brands.AnyAsync(b => !b.IsDeleted && b.Id == product.BrandId))
            {
                ModelState.AddModelError("BrandId", "Please Select A Correct Brand");
                return View();
            }
            if (!await _context.Categories.AnyAsync(c => !c.IsDeleted && c.Id == product.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Please Select A Correct Category");
                return View();
            }
            if (!await _context.Genders.AnyAsync(g => !g.IsDeleted && g.Id == product.GenderId))
            {
                ModelState.AddModelError("GenderId", "Please Select A Correct Gender");
                return View();
            }
            if (product.TagIds != null && product.TagIds.Count > 0)
            {
                List<ProductTags> productTags = new List<ProductTags>();

                foreach (int tagId in product.TagIds)
                {
                    if (!await _context.Tags.AnyAsync(t => !t.IsDeleted && t.Id == tagId))
                    {
                        ModelState.AddModelError("TagIds", $"Tag Id {tagId} Is InCorrec");
                        return View();
                    }

                    ProductTags productTag = new ProductTags
                    {
                        TagId = tagId
                    };

                    productTags.Add(productTag);
                }

                product.ProductTags = productTags;
            }
            else
            {
                ModelState.AddModelError("TagIds", "Tags Is Requered");
                return View();
            }

            if (product.MainFile != null)
            {
                if (product.MainFile.CheckFileContentType("image/jpeg"))
                {
                    ModelState.AddModelError("MainFile", "Please Select Correct Image Type. Example Jpeg or Jpg");
                    return View();
                }
                if (product.MainFile.CheckFileSize(50))
                {
                    ModelState.AddModelError("MainFile", "Please Select Coorect Image Size. Maximum 50 KB");
                    return View();
                }
                product.Image = await product.MainFile.CreateFileAsync(_env, "assets", "img", "product");
            }
            else
            {
                ModelState.AddModelError("MainFile", "Please Select Image");
                return View();
            }
            if (product.Files != null && product.Files.Count > 0)
            {
                if (product.Files.Count > 5)
                {
                    ModelState.AddModelError("Files", "Can You Select Maximum 5 Images");
                    return View();
                }
                List<ProductImages> productImages = new List<ProductImages>();
                foreach (IFormFile file in product.Files)
                {
                    if (file.CheckFileContentType("image/jpeg"))
                    {
                        ModelState.AddModelError("Files", "Please Select A Correct Image type. Example Jpeg Or Jpg");
                        return View();
                    }
                    if (file.CheckFileSize(50))
                    {
                        ModelState.AddModelError("Files", "Please Select A Correct Image Size. Maximum 50 KB");
                        return View();
                    }
                    ProductImages images = new ProductImages
                    {
                        Image = await file.CreateFileAsync(_env, "assets", "img", "product-quick")
                    };
                    productImages.Add(images);
                }
                product.ProductImages = productImages;

            }
            else
            {
                ModelState.AddModelError("Files", "Please Select Images");
                return View();
            }
            if (product.Count>0)
            {
                product.Availability = true;
            }
            product.Name = product.Name.Trim();
            product.FacebookUrl = product.FacebookUrl.Trim();
            product.TwitterUrl = product.TwitterUrl.Trim();
            product.PinterestUrl = product.PinterestUrl.Trim();
            product.GooglePlusUrl = product.GooglePlusUrl.Trim();
            product.Createdat = DateTime.UtcNow.AddHours(4);
            await _context.Products.AddAsync(product);
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
            Product product = await _context.Products.Include(p=>p.ProductTags).Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
            if (product == null)
            {
                return NotFound();
            }
            product.TagIds =  product.ProductTags.Select(t => t.TagId).ToList();

            ViewBag.Brands = await _context.Brands.Where(b => !b.IsDeleted).ToListAsync();
            ViewBag.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            ViewBag.Genders = await _context.Genders.Where(g => !g.IsDeleted).ToListAsync();
            ViewBag.Tags = await _context.Tags.Where(t => !t.IsDeleted).ToListAsync();
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Product product)
        {
            ViewBag.Brands = await _context.Brands.Where(b => !b.IsDeleted).ToListAsync();
            ViewBag.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
            ViewBag.Genders = await _context.Genders.Where(g => !g.IsDeleted).ToListAsync();
            ViewBag.Tags = await _context.Tags.Where(t => !t.IsDeleted).ToListAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id == null)
            {
                return BadRequest();
            }
            if (id != product.Id)
            {
                return BadRequest();
            }
            Product dbproduct = await _context.Products.Include(p => p.ProductTags).Include(p => p.ProductImages).FirstOrDefaultAsync(p => !p.IsDeleted && p.Id == product.Id);
            if (dbproduct == null)
            {
                return NotFound();
            }
            if (!await _context.Brands.AnyAsync(b => !b.IsDeleted && b.Id == product.BrandId))
            {
                ModelState.AddModelError("BrandId", "Please Select A Correct Brand");
                return View();
            }
            if (!await _context.Categories.AnyAsync(c => !c.IsDeleted && c.Id == product.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Please Select A Correct Category");
                return View();
            }
            if (!await _context.Genders.AnyAsync(g => !g.IsDeleted && g.Id == product.GenderId))
            {
                ModelState.AddModelError("GenderId", "Please Select A Correct Gender");
                return View();
            }

            if (product.TagIds != null && product.TagIds.Count > 0)
            {
                if (dbproduct.ProductTags != null && dbproduct.ProductTags.Count() > 0)
                {
                    _context.ProductTags.RemoveRange(dbproduct.ProductTags);
                }

                List<ProductTags> productTags = new List<ProductTags>();

                foreach (int tagId in product.TagIds)
                {
                    if (!await _context.Tags.AnyAsync(t => !t.IsDeleted && t.Id == tagId))
                    {
                        ModelState.AddModelError("TagIds", $"Tag Id {tagId} Is InCorrec");
                        return View();
                    }

                    ProductTags productTag = new ProductTags
                    {
                        TagId = tagId
                    };

                    productTags.Add(productTag);
                }

                dbproduct.ProductTags = productTags;
            }
            else
            {
                ModelState.AddModelError("TagIds", "Tags Is Requered");
                return View();
            }


            if (product.MainFile != null)
            {
                if (product.MainFile.CheckFileContentType("image/jpeg"))
                {
                    ModelState.AddModelError("MainFile", "Please Select Correct Image Type. Example Jpeg or Jpg");
                    return View();
                }
                if (product.MainFile.CheckFileSize(50))
                {
                    ModelState.AddModelError("MainFile", "Please Select Correct Image Size. Maximum 50 KB");
                    return View();
                }
                string fullpath = Path.Combine(_env.WebRootPath, "assets", "img", "product", dbproduct.Image);
                if (System.IO.File.Exists(fullpath))
                {
                    System.IO.File.Delete(fullpath);
                }
                dbproduct.Image = await product.MainFile.CreateFileAsync(_env, "assets", "img", "product");
            }
            if (product.Files != null && product.Files.Count > 0)
            {
                int selectedimage = 5 - dbproduct.ProductImages.Count;
                if (product.Files != null && product.Files.Count > 0 && selectedimage < product.Files.Count)
                {
                    ModelState.AddModelError("Files", $" You Can Select {selectedimage} Image");
                    return View(dbproduct);
                }
                List<ProductImages> productImages = new List<ProductImages>();
                foreach (IFormFile file in product.Files)
                {
                    if (file.CheckFileContentType("image/jpeg"))
                    {
                        ModelState.AddModelError("Files", "Please Select A Correct Image Type. Exaple Jpg or Jpeg.");
                        return View();
                    }
                    if (file.CheckFileSize(50))
                    {
                        ModelState.AddModelError("Files", "Please Select A Correct Image Size. Maximum 50 KB");
                        return View();
                    }
                    ProductImages images = new ProductImages
                    {
                        Image = await file.CreateFileAsync(_env, "assets", "img", "product-quick")
                    };
                    productImages.Add(images);
                }
                if (dbproduct.ProductImages != null && dbproduct.ProductImages.Count() >= 0)
                {
                    dbproduct.ProductImages.AddRange(productImages);
                }
                else
                {
                    dbproduct.ProductImages = productImages;
                }
            }
            if (dbproduct.Count>0)
            {
                dbproduct.Availability = true;
            }
            dbproduct.Name = product.Name.Trim();
            dbproduct.FacebookUrl = product.FacebookUrl.Trim();
            dbproduct.TwitterUrl = product.TwitterUrl.Trim();
            dbproduct.PinterestUrl = product.PinterestUrl.Trim();
            dbproduct.GooglePlusUrl = product.GooglePlusUrl.Trim();
            product.UpdatedAt = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();
            return RedirectToAction("index");
        }
        [HttpGet]
        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            ProductImages productImage = await _context.ProductImages.Include(p => p.Product).ThenInclude(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == id);

            if (productImage == null)
            {
                return NotFound();
            }
            if (productImage.Product.ProductImages.Count == 1)
            {
                return PartialView("_ProductImagePartial", productImage.Product.ProductImages.ToList());
            }
            string fullpath = Path.Combine(_env.WebRootPath, "assets", "img", "product=quick", productImage.Image);
            if (System.IO.File.Exists(fullpath))
            {
                System.IO.File.Delete(fullpath);
            }
            _context.ProductImages.Remove(productImage);
            await _context.SaveChangesAsync();
            return PartialView("_ProductImagePartial", productImage.Product.ProductImages.ToList());

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Product product = await _context.Products.FirstOrDefaultAsync(p => !p.IsDeleted && p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            product.IsDeleted = true;
            product.DeletedAt = DateTime.UtcNow.AddHours(4);

            await _context.SaveChangesAsync();
            return PartialView("_ProductIndexPartial", await _context.Products.ToListAsync());
        }
        public async Task<IActionResult> Restore(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Product product = await _context.Products.FirstOrDefaultAsync(p => p.IsDeleted && p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            product.IsDeleted = false;
            product.DeletedAt = null;

            await _context.SaveChangesAsync();
            return PartialView("_ProductIndexPartial", await _context.Products.ToListAsync());
        }
    }
}
