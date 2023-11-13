using BE1109.FilterAtribute;
using BE1109.Models;
using DataAccess.Computer.DO;
using DataAccess.Computer.IServices;
using DataAccess.Computer.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using OfficeOpenXml;
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


                //if (cacheValue != null)
                //{
                //    var cachedDataString = Encoding.UTF8.GetString(cacheValue);
                //    if (cachedDataString != null)
                //    {
                //        list = JsonConvert.DeserializeObject<List<Product>>(cachedDataString.ToString());
                //    }

                //    return Ok(list);

                //}

              //  list = await _myShopUnitOfWork._productRepository.GetProducts();
                list = await _myShopUnitOfWork._productRepository.GetProducts_Dapper();
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
               // var result = await _myShopUnitOfWork._productRepository.ProductInsert(requestData);
                var result = await _myShopUnitOfWork._productRepository.ProductInsert_Dapper(requestData);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost("ProductImportByExcel")]
        public async Task<ActionResult> ProductImportByExcel([FromForm] UploadFileInputDto formFile)
        {
            try
            {
                if (formFile == null || formFile.File.Length <= 0)
                {
                    throw new Exception("file dữ liệu không được trống");
                }

                if (!Path.GetExtension(formFile.File.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    throw new Exception("Hệ thống chỉ hỗ trợ file .xlsx");
                }

                using (var stream = new MemoryStream())
                {
                    await formFile.File.CopyToAsync(stream);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.Commercial;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 3; row <= rowCount; row++)
                        {
                            var name = worksheet.Cells[row, 2]?.Value?.ToString()?.Trim();
                        }
                    }
                }
            }
            catch (Exception e)
            {

                throw;
            }

            return Ok();
        }


        [HttpPost("Export")]
        public async Task<IActionResult> Export([FromBody] ProductGetListRequestData requestData)
        {
            var contentRoot = _config["TemplateEXCEL"] ?? "";
            var webRoot = Path.Combine(contentRoot, "Template");
            var templateFileInfo = new FileInfo(Path.Combine(contentRoot, "CheckDailyTemplate.xlsx"));
            var packageReport = await DesignWorkSheetExportAsync(requestData, templateFileInfo);

            return File(packageReport, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
        }

        private async Task<byte[]?> DesignWorkSheetExportAsync(ProductGetListRequestData requestData, FileInfo path)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            using (var package = new ExcelPackage(path))
            {
                //Tạo mới package execl
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Data");
                var list = await _myShopUnitOfWork._productRepository.GetProducts();


                worksheet.Cells["A1:Q1"].Merge = true;
                worksheet.Cells["A1:Q1"].Value = "Export";
                worksheet.Cells["A2"].Value = "PRODUCTNAME";
                worksheet.Cells["B2"].Value = "DonViTinh";
                worksheet.Cells["C2"].Value = "DonGia";
                worksheet.Cells["D2"].Value = "lINK";
                int start_row_index = 3;
                int total = list.Count;

                foreach (var item in list)
                {
                    worksheet.Cells[start_row_index, 1].Value = item.ProductName;
                    worksheet.Cells[start_row_index, 2].Value = item.DonViTinh;
                    worksheet.Cells[start_row_index, 3].Value = item.DonGia;
                    start_row_index++;
                   
                }

                package.Save();
                return package.GetAsByteArray();
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
