using BackEndProjectJuan.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string SurName { get; set; }
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required]
        [StringLength(255)]
        public string Address { get; set; }
        [Required]
        [StringLength(255)]
        public string Country { get; set; }
        [Required]
        [StringLength(255)]
        public string City { get; set; }
        public string ZipCode { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName ="money")]
        public double TotalPrice { get; set; }
        [Required]
        [StringLength(255)]
        public string Comment { get; set; }

        public AppUser AppUser { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
