using BE1109.Models;
using DataAccess.Computer.DO;
using DataAccess.Computer.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BE1109.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productServices;
        public ProductController(IProductRepository productServices)
        {
            _productServices = productServices;
        }


        [HttpPost("Product_GetList")]

        public async Task<ActionResult> Product_GetList()
        {
            try
            {
                var userLogin = GetCurrentUser();
                if (userLogin.UserID <= 0)
                {
                    return BadRequest();
                }

                var result = await _productServices.GetProducts();
                return Ok(new { items = result });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost("ProductInsert")]
        public async Task<ActionResult> ProductInsert(Product product)
        {
            try
            {

                await Task.Delay(300);
                var result = await _productServices.ProductInsert(product);
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
