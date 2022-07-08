﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Models
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public bool Availability { get; set; }
        [Required]
        [StringLength(255)]
        public string Description { get; set; }
    }
}
