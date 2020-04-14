using Emarket.Models;
using Emarket.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
namespace Emarket.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;
        public HomeController()
        {
            db = new ApplicationDbContext();
        }
        //public ActionResult Index(string query = null)
        //{
        //    var product = db.Products.ToList();
        //    if (!String.IsNullOrWhiteSpace(query))
        //    {
        //      var  search = db.Products.Where(p =>
        //             p.Name.Contains(query) ||
        //             p.Category.Name.Contains(query));

        //    }
        //    return View(product);
        //}
        public ActionResult Index(string query = null)
        {
            var upcomingProducts = db.Products.Include(p=>p.Category);
       


            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingProducts = upcomingProducts.Where(p =>
                       p.Name.Contains(query) ||
                       p.Category.Name.Contains(query));

            }




            var viewModel = new ProductViewModel()
            {
                UpcomingProducts = upcomingProducts,

                SearchTerm=query
                

            };
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}