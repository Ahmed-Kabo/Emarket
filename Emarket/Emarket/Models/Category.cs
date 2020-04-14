using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Emarket.Models
{
    public class Category
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public int NumberOfProduct { get; set; }
    }
}