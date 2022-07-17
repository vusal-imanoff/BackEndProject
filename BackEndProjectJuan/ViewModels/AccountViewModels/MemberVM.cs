using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndProjectJuan.Models;

namespace BackEndProjectJuan.ViewModels.AccountViewModels
{
    public class MemberVM
    {

        public ProfileVM ProfileVM { get; set; }
        public List<Order> Orders { get; set; }
    }
}
