using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public double Price { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public double DiscountPrice { get; set; }
        public bool IsDeleted { get; set; }
        public int ProductDetailId { get; set; }
        public ProductDetail ProductDetail { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
