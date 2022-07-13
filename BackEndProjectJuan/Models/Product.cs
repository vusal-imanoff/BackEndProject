using Microsoft.AspNetCore.Http;
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
        [StringLength(255)]
        public string Image { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public double Price { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public double DiscountPrice { get; set; }
        public int Count { get; set; }
        public bool Availability { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
        [Required]

        public string FacebookUrl  { get; set; }
        [Required]
        public string TwitterUrl  { get; set; }
        [Required]
        public string PinterestUrl  { get; set; }
        [Required]
        public string GooglePlusUrl  { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> Createdat { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }

        public List<ProductImages> ProductImages  { get; set; }
        public IEnumerable<ProductSocialAddress> ProductSocialAddresses { get; set; }
        [NotMapped]
        public IFormFile MainFile { get; set; }
        [NotMapped]
        public List<IFormFile> Files { get; set; }
    }
}
