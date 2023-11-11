using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMvcDemo.Models;
using WebMvcDemo.Models.Product;

namespace WebMvcDemo.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductListPartial(ProductListRequestData requestData)
        {
            var lst = new List<ProductList>();
            try
            {
                // Khai báo url Server
                var url = "http://localhost:5235/api/";
                var base_url = "Product/Product_GetList";
                var token = Request.Cookies["BE_1109_Token"] != null ? Request.Cookies["BE_1109_Token"].Value : string.Empty;

                //Đữa dữ liệu từ Object (class) sang Json
                var jsondata = JsonConvert.SerializeObject(requestData);

                // Gọi Server để lấy kết quả
                var result_from_server = Computer.Common.HttpRequest
                    .WebPostWithToken(url, base_url, jsondata, requestData.token);

                // Server ko trả về kết quả 
                if (string.IsNullOrEmpty(result_from_server))
                {
                    return PartialView(lst);
                }

                // Server trả về kết quả thì đem dữ liệu ở dạng Json convert sang Object để hiển thị
                var result_data = JsonConvert.DeserializeObject<ProductListResponseData>(result_from_server);
                if (result_data != null && result_data.items.Count > 0)
                {
                    lst = result_data.items;
                }

            }
            catch (Exception ex)
            {

                throw;
            }

            return PartialView(lst);
        }


        public ActionResult ProductInsert(ProductInsertRequestData requestData)
        {
            var returnData = new ReturnData();
            try
            {
                var url = "http://localhost:5235/api/";
                var base_url = "Product/ProductInsert";
                var token = Request.Cookies["BE_1109_Token"] != null ? Request.Cookies["BE_1109_Token"].Value : string.Empty;

                //Đữa dữ liệu từ Object (class) sang Json
                var jsondata = JsonConvert.SerializeObject(requestData);

                // Gọi Server để lấy kết quả
                var result_from_server = Computer.Common.HttpRequest
                    .WebPostWithToken(url, base_url, jsondata, token);



                // Server trả về kết quả thì đem dữ liệu ở dạng Json convert sang Object để hiển thị
                var result_data = JsonConvert.DeserializeObject<ReturnData>(result_from_server);
                returnData.ResponseCode = result_data.ResponseCode;
                returnData.Description = result_data.Description;
                return Json(returnData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }

            return Json(returnData, JsonRequestBehavior.AllowGet);
        }
    }

    public class HoaDonDienTu
    {
        public string key { get; set; }
        public string content { get; set; }
    }
}