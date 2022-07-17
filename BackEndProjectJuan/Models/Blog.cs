using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string MainDescription { get; set; }
        [Required]
        [StringLength(255)]
        public string SubDescription { get; set; }
        [Required]
        [StringLength(255)]
        public string Blacknote { get; set; }
        [StringLength(255)]
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<DateTime> CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
    }

}
