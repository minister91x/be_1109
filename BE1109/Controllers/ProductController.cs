using BE1109.FilterAtribute;
using BE1109.Models;
using DataAccess.Computer.DO;
using DataAccess.Computer.IServices;
using DataAccess.Computer.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        public ProductController(IMyShopUnitOfWork myShopUnitOfWork)
        {
            _myShopUnitOfWork = myShopUnitOfWork;
        }

        [HttpPost("Product_GetList")]
        // [MyShopAuthorize("PRODUCT_GETLIST", "VIEW")]
        public async Task<ActionResult> Product_GetList()
        {
            try
            {
                // var result = await _myShopUnitOfWork._productRepository.GetProducts();
                var result = _myShopUnitOfWork._productGenericRepository.GetAll();
                return Ok(new { items = result });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost("ProductInsert")]
        [MyShopAuthorize("PRODUCT_INSERT", "INSERT")]
        public async Task<ActionResult> ProductInsert(Product product)
        {
            try
            {

                await Task.Delay(300);
                // var result = await _myShopUnitOfWork._productRepository.ProductInsert(product);
                var result = await _myShopUnitOfWork._productGenericRepository.Add(product);
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
