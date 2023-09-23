using DataAccess.Demo.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMvcDemo.Filter;
using WebMvcDemo.Models;

namespace WebMvcDemo.Controllers
{
    [Log]
    public class HomeController : Controller
    {

        public ActionResult Index(int? id)
        {
            var lst = new List<Category>();
            try
            {
                var lsst = new DataAccess.Demo.DAOImpl.CategoryDAOImpl();
                lsst.GetProducts();
            }
            catch (Exception ex)
            {

                throw;
            }
            return View(lst);
        }


        public ActionResult Test()
        {
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About(string name)
        {
            ViewBag.Message = "Your Name:" + name;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult DemoPartialView()
        {
            return PartialView();
        }
    }
}