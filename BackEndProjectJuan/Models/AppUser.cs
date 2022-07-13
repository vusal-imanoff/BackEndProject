using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string Surname { get; set; }
        [Required]
        [StringLength(255)]
        public string FatherName { get; set; }
        [Required]
        public int Age { get; set; }
    }
}
