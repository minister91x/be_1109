using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMvcDemo.Models;

namespace WebMvcDemo.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public JsonResult Account_Login(AccountLoginRequestData requestData)
        {
            var returnData = new AccountLoginResponseData();
            try
            {
                if (requestData == null
                    || string.IsNullOrEmpty(requestData.UserName)
                    || string.IsNullOrEmpty(requestData.PassWord))
                {
                    returnData.code = -1;
                    returnData.desciption = "Vui lòng điền đẩy đủ thông tin";
                    return Json(returnData, JsonRequestBehavior.AllowGet);
                }

                var url = "http://localhost:5235/api/";
                var base_url = "Account/Login";

                // Đưa object thành JSON 
                var passwordHash = Computer.Common.Security.MD5Hash(requestData.PassWord);
                requestData.PassWord = passwordHash;
                var jsondata = JsonConvert.SerializeObject(requestData);

                // gửi dữ liệu lên server để lấy thông tin
                var content_from_Server = Computer.Common.HttpRequest.WebPost(url, base_url, jsondata);

                // nhận kết quả trả về từu server và convert dữ liệu từ server trả về
                // sang object AccountLoginResponseData
                returnData = JsonConvert.DeserializeObject<AccountLoginResponseData>(content_from_Server);

                return Json(returnData, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}