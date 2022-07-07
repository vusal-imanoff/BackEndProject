using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Models
{
    public class Slider
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Image { get; set; }
        [Required]
        [StringLength(255)]
        public string MainTitle { get; set; }
        [Required]
        [StringLength(255)]
        public string SubTitle { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        [StringLength(255)]
        public string RedirectUrl { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }

    }
}
