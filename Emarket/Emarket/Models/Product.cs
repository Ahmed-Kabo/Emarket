using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emarket.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public Category Category { get; set; }

        public byte CategoryId { get; set; }
       
       
        
    }
}