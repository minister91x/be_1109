using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvcDemo.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        //[NonAction]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index(int? id)
        {
            return Json(new { });

            //return PartialView();
            // return File()
            // return Content("abc");

        }

        [HttpPost]
       
        public JsonResult ActionNameDemo()
        {
            return Json(new { code = 1, mes = "Xin chào" }, JsonRequestBehavior.AllowGet);
        }


    }
}