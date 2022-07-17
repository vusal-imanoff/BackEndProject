using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        [Column(TypeName ="money")]
        public double Price { get; set; }
        public int Count { get; set; }
        [Column(TypeName = "money")]
        public double TotalPrice { get; set; }

        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
