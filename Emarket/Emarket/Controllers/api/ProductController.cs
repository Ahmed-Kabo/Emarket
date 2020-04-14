using Emarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
namespace Emarket.Controllers.api
{
    public class ProductController : ApiController
    {
        private ApplicationDbContext db;
        public ProductController()
        {
            db = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            var product = db.Products.SingleOrDefault(p => p.Id == id && p.UserId == userId);
            var cindb = db.Categories.Single(c => c.Id == product.CategoryId);
            cindb.NumberOfProduct--;
            if (product == null)
                return NotFound();
            db.Products.Remove(product);
            db.SaveChanges();
            return Ok();
        }
    }
}
