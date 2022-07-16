using BackEndProjectJuan.DAL;
using BackEndProjectJuan.Interfaces;
using BackEndProjectJuan.Models;
using BackEndProjectJuan.ViewModels.Basket;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContext;
        public LayoutService(AppDbContext context, IHttpContextAccessor httpContext)
        {
            _context=context;
            _httpContext = httpContext;
        }

        public async Task<List<BasketVM>> GetBasketAsync()
        {
            string basket = _httpContext.HttpContext.Request.Cookies["basket"];
            List<BasketVM> basketVMs = null;
            if (!string.IsNullOrWhiteSpace(basket))
            {
                basketVMs = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            }
            else
            {
                basketVMs = new List<BasketVM>();
            }
            foreach (BasketVM basketVM in basketVMs)
            {
                Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == basketVM.Id);

                basketVM.Image = product.Image;
                basketVM.Price = product.DiscountPrice > 0 ? product.DiscountPrice : product.Price;
                basketVM.Extax = product.Extax;
                basketVM.Name = product.Name;
            }

            return basketVMs;

        }

        public async Task<IDictionary<string, string>> GetSettingsAsync()
        {
            IDictionary<string, string> settings = await _context.Settings.ToDictionaryAsync(d => d.Key, d => d.Value);
            return settings;
        }
    }
}
