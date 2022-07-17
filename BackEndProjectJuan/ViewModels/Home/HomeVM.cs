using BackEndProjectJuan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.ViewModels.Home
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Product> Products { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
