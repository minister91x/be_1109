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

            return View();
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
            var lst = new List<CategoryViewModel>();
            try
            {
                var lsst = new DataAccess.Demo.DAOImpl.CategoryDAOImpl();
                var domainModels = lsst.GetProducts();
                if (domainModels.Count > 0)
                {
                    foreach (var item in domainModels)
                    {
                        var viewModel = new CategoryViewModel();
                        viewModel.CategoryId = item.CategoryId;
                        viewModel.CategoryName = item.CategoryName;
                        viewModel.CategoryTypeName = item.CategoryType == 0
                            ? "Loại 1" : item.CategoryType == 2 ? "Loại 2" : "Loại 3";
                        lst.Add(viewModel);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return PartialView(lst);
        }

        public ActionResult CategoryInsertUpdate(CategoryInsertModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Nếu = false dữ liệu không hợp lệ
                    // Nếu = true dữ liệu hợp lệ
                }
            }
            catch (Exception)
            {

                throw;
            }

            return Json(new { code = 1, mes = "ok" }, JsonRequestBehavior.AllowGet);
        }
    }
}