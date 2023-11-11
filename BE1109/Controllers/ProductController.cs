using BE1109.FilterAtribute;
using BE1109.Models;
using DataAccess.Computer.DO;
using DataAccess.Computer.IServices;
using DataAccess.Computer.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace BE1109.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // private IProductRepository _productServices;
        private IMyShopUnitOfWork _myShopUnitOfWork;
        //public ProductController(IProductRepository productServices)
        //{
        //    _productServices = productServices;
        //}

        private IConfiguration _config;

        private readonly IDistributedCache _cache;

        public ProductController(IMyShopUnitOfWork myShopUnitOfWork, IConfiguration config,
            IDistributedCache cache)
        {
            _myShopUnitOfWork = myShopUnitOfWork;
            _config = config;
            _cache = cache;
        }

        [HttpPost("Product_GetList")]
        // [MyShopAuthorize("PRODUCT_GETLIST", "VIEW")]
        public async Task<ActionResult> Product_GetList()
        {
            var list = new List<Product>();
            try
            {
                var key_cache = "GET_LIST_PRODUCT";
                var cacheValue = await _cache.GetAsync(key_cache);

                // var result = await _myShopUnitOfWork._productRepository.GetProducts();
               

                if (cacheValue != null)
                {
                    var cachedDataString = Encoding.UTF8.GetString(cacheValue);
                    if (cachedDataString != null)
                    {
                        list = JsonConvert.DeserializeObject<List<Product>>(cachedDataString.ToString());
                    }

                    return Ok(list);

                }

                 list = await _myShopUnitOfWork._productRepository.GetProducts();

                if (list.Count > 0)
                {
                    // lưu cache 
                    var dataCache = JsonConvert.SerializeObject(list);

                    var dataToCache = Encoding.UTF8.GetBytes(dataCache);

                    DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(5))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(3));

                    await _cache.SetAsync(key_cache, dataToCache, options);
                }

                return Ok(new { items = list });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost("ProductInsert")]
        //[MyShopAuthorize("PRODUCT_INSERT", "INSERT")]
        public async Task<ActionResult> ProductInsert(Product requestData)
        {
            try
            {

                await Task.Delay(300);
                // var result = await _myShopUnitOfWork._productRepository.ProductInsert(product);

                // Gọi media để lấy tên ảnh từ bASE64 
                var url_api = _config["MEDIA:URL"] ?? "http://localhost:26767/";
                var base_url = "Upload/UploadProductImage";

                //var secretKey = _config["Sercurity:secretKeyCall_API"] ?? "UxFkTt5siR5dibph8JdUIsixJ2mmhr";
                //var Sign = Computer.Common.Security.MD5Hash(requestData.base64Image + "|" + secretKey);
                //requestData.sign = Sign;


                //var dataJson = JsonConvert.SerializeObject(requestData);

                ////var token = Request.Cookies["TOKEN_SERVER"] != null ? Request.Cookies["TOKEN_SERVER"].Value : string.Empty;
                //var imageName = string.Empty;
                //var result_media = Computer.Common.HttpRequest.WebPost(url_api, base_url, dataJson);

                //if (result_media != null)
                //{
                //    var rs = JsonConvert.DeserializeObject<MediaReturnData>(result_media);
                //    imageName = rs.Description;
                //}

                //requestData.base64Image = imageName;
                var result = await _myShopUnitOfWork._productRepository.ProductInsert(requestData);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserModel
                {
                    UserID = Convert.ToInt32(userClaims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid)?.Value),
                    UserName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                    FullName = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value,
                };

            }
            return null;
        }
    }
}
