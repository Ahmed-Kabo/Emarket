using Emarket.Models;
using Emarket.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
namespace Emarket.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db;
        public ProductController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var product = db.Products
                .Where(g => g.UserId == userId )
                .Include(g => g.Category)
                .ToList();
            return View(product);
        }

        [HttpPost]
        public ActionResult Search(ProductViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var products = db.Products.Single(p => p.Id == id && p.UserId == userId);

            var viewmodel = new ProductFormViewModel()
            {
                Id = id,
                Categories = db.Categories.ToList(),
                Name = products.Name,
                Description = products.Description,
                Price=products.Price,
                Category = products.CategoryId,
                Image = products.Image,
                Heading = "Edit Product"
            };
            return View("ProductForm", viewmodel);
        }
        [Authorize]
        public ActionResult Create()
        {
            var viewmodel = new ProductFormViewModel
            {
                Categories = db.Categories.ToList(),
                Heading = "Add Product"

            };
            return View("ProductForm", viewmodel);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(ProductFormViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                viewmodel.Categories = db.Categories.ToList();
                return View("ProductForm", viewmodel);
            }
            if (viewmodel.ImageUpload != null)
            {
                var path = Path.Combine(Server.MapPath("~/Images"), viewmodel.ImageUpload.FileName);
                viewmodel.ImageUpload.SaveAs(path);
                viewmodel.Image = viewmodel.ImageUpload.FileName;

            }

            
            var userId = User.Identity.GetUserId();
            var product = new Product()
            {
                Name = viewmodel.Name,
                Description = viewmodel.Description,
                Image = viewmodel.Image,
                CategoryId = viewmodel.Category,
                Price=viewmodel.Price,
                
                UserId = userId
                
                
            

            };
         
            
            var cindb = db.Categories.Single(c => c.Id == product.CategoryId);
            cindb.NumberOfProduct++;
            
            
            
            
            db.Products.Add(product);
            
            
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ProductFormViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                viewmodel.Categories = db.Categories.ToList();
                return View("ProductForm", viewmodel);

            }

            var userId = User.Identity.GetUserId();
            var product = db.Products
                .Single(p => p.Id == viewmodel.Id && p.UserId == userId);

            product.Name = viewmodel.Name;
            product.Description = viewmodel.Description;
            product.Price = viewmodel.Price;
            product.Image = viewmodel.Image;
            product.CategoryId = viewmodel.Category;


            var oldpath = Path.Combine(Server.MapPath("~/Images"), product.Image);
            if (viewmodel.ImageUpload != null)
            {
                System.IO.File.Delete(oldpath);
                var path = Path.Combine(Server.MapPath("~/Images"), viewmodel.ImageUpload.FileName);
                viewmodel.ImageUpload.SaveAs(path);

                product.Image = viewmodel.ImageUpload.FileName;
            }



            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Details(int id)
        {
            var product = db.Products.Include(p => p.Category).SingleOrDefault(n => n.Id == id);

            if (product == null)
                return HttpNotFound();

            return View(product);
        }
    }
}