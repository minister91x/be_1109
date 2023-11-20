using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMvcDemo.Models;

namespace WebMvcDemo.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            var list_cart = new List<ShoppingCart>();
            try
            {
                var cart = Request.Cookies["MyShoppingCart"] != null ? Request.Cookies["MyShoppingCart"].Value : string.Empty;
               
                list_cart = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ShoppingCart>>(HttpUtility.UrlDecode(cart));
            }
            catch (Exception ex)
            {

                throw;
            }
            return View(list_cart);
        }
    }
}