using BackEndProjectJuan.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<SocialAddress> SocialAddresses { get; set; }
        public DbSet<ProductSocialAddress> ProductSocialAddresses { get; set; }
        public DbSet<ProductItems> ProductItems { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Gender> Genders { get; set; }
    }
}
