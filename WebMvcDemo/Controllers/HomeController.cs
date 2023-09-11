using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMvcDemo.Models;

namespace WebMvcDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? id)
        {
            return RedirectToAction("About", "Home");
            var listModel = new StudentModels_TrungGian();
            var list = new List<StudentModels>();
            var listNew = new List<StudentModels_New>();
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    var model = new StudentModels
                    {
                        Id = i,
                        Name = "NGUYEN VAN" + i
                    };

                    list.Add(model);
                }

                for (int i = 0; i < 10; i++)
                {
                    var model2 = new StudentModels_New
                    {
                        Id = i,
                        Name = "NGUYEN VAN NEW" + i
                    };

                    listNew.Add(model2);
                }


                listModel.studentModels = list;
                //listModel.StudentModels_New = listNew;

                ViewBag.studentModels = listNew;
            }
            catch (Exception ex)
            {

                throw;
            }
            return View(listModel);
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
    }
}