using DataAccess.Computer.DO;
using DataAccess.Computer.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                var result = await _productServices.GetProducts();
                return Ok(result);
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
                var result = await _productServices.ProductInsert(product);
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
