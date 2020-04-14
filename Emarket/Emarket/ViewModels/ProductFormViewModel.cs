using Emarket.Controllers;
using Emarket.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Emarket.ViewModels
{
    public class ProductFormViewModel
    {

        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        [Required]
        public byte Category { get; set; }

        [Required]
        public double Price { get; set; }

        public string Heading { get; set; }

        [Required]
        public string Image { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }
        public int NumberOfProduct { get; set; }

        public ProductFormViewModel()
        {
            if (Image == null)
            {
                Image = "camerashot.png";
            }
        }
        public string Action
        {
            get
            {
                Expression<Func<ProductController, ActionResult>>
                    update = (c => c.Update(this));

                Expression<Func<ProductController, ActionResult>>
                    create = (c => c.Create(this));
                var action = (Id != 0) ? update : create;

                return (action.Body as MethodCallExpression).Method.Name;
            }
        }
    }
}
