using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Models
{
    public class ProductImages
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Image { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
