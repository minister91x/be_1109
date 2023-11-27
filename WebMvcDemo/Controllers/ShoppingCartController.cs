using Newtonsoft.Json;
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

        public JsonResult CheckOut(Customer customer)
        {
            var returnData = new ReturnData();
            var listOrderItems = new List<OrderRequest>();
            try
            {
                var cart = Request.Cookies["MyShoppingCart"] != null ? Request.Cookies["MyShoppingCart"].Value : string.Empty;

                var list_cart = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ShoppingCart>>(HttpUtility.UrlDecode(cart));

                if (list_cart == null)
                {
                    returnData.ResponseCode = -1;
                    returnData.Description = "chưa có sản phẩm nào trong giỏ hàng!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }

                foreach (var item in list_cart)
                {
                    listOrderItems.Add(new OrderRequest
                    {
                        ProductID = item.ProductId,
                        Quantity = item.Quantity
                    });
                }

               
                var createOderReq = new CreateOrderRequestData
                {
                    customer = customer,
                    orderItems = listOrderItems
                };

                // Khai báo url Server
                var url = "http://localhost:5235/api/";

                var base_url = "ShoppingCart/OrderInsert";
                var token = Request.Cookies["BE_1109_Token"] != null ? Request.Cookies["BE_1109_Token"].Value : string.Empty;

                //Đữa dữ liệu từ Object (class) sang Json
                var jsondata = JsonConvert.SerializeObject(createOderReq);

                // Gọi Server để lấy kết quả
                var result_from_server = Computer.Common.HttpRequest
                    .WebPostWithToken(url, base_url, jsondata, createOderReq.token);

                // Server ko trả về kết quả 
                if (string.IsNullOrEmpty(result_from_server))
                {
                    returnData.ResponseCode = -2;
                    returnData.Description = "Tạo đơn hàng thất bại!";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }

                // Server trả về kết quả thì đem dữ liệu ở dạng Json convert sang Object để hiển thị
                returnData = JsonConvert.DeserializeObject<ReturnData>(result_from_server);

            }
            catch (Exception ex)
            {
                returnData.ResponseCode = -969;
                returnData.Description = ex.Message;
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }

            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
    }
}