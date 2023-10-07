using DataAccess.Computer.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE1109.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductServices _productServices;
        public ProductController(IProductServices productServices)
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





    }
}
