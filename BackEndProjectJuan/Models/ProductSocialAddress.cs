using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProjectJuan.Models
{
    public class ProductSocialAddress
    {
        public int Id { get; set; }
        public int ProcuctId { get; set; }
        public Product Product { get; set; }
        public int SocialAddressId { get; set; }
        public SocialAddress SocialAddress { get; set; }
        [Required]
        public string Url { get; set; }
    }
}
