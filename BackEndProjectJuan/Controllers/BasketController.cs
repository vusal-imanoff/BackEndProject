using BackEndProjectJuan.DAL;
using BackEndProjectJuan.Models;
using BackEndProjectJuan.ViewModels.Basket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;
        public BasketController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            string basket = Request.Cookies["basket"];
            List<BasketVM> basketVMs = null;

            if (!string.IsNullOrWhiteSpace(basket))
            {
                basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            }
            else
            {
                basketVMs = new List<BasketVM>();
            }
            return View( await _getBasketAsync(basket));
        }

        public async Task<IActionResult> GetBasket()
        {
            string basket = Request.Cookies["basket"];
            if (string.IsNullOrWhiteSpace(basket))
            {
                return BadRequest();
            }
            return PartialView("_BasketPartial", await _getBasketAsync(basket));
        }

        private async Task<List<BasketVM>> _getBasketAsync(string cookie)
        {
            List<BasketVM> basketVMs = null;
            if (!string.IsNullOrWhiteSpace(cookie))
            {
                basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(cookie);
            }
            else
            {
                basketVMs = new List<BasketVM>();
            }

            foreach (BasketVM basket in basketVMs)
            {
                Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == basket.Id);
                basket.Image = product.Image;
                basket.Price = product.DiscountPrice > 0 ? product.DiscountPrice : product.Price;
                basket.Extax = product.Extax;
                basket.Name = product.Name;
            }
            return basketVMs;
        }
    }
}
