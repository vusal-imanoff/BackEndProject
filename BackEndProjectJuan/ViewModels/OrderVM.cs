using BackEndProjectJuan.Models;
using BackEndProjectJuan.ViewModels.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.ViewModels
{
    public class OrderVM
    {
        public Order Order { get; set; }
        public List<BasketVM> BasketVMs { get; set; }
    }
}
